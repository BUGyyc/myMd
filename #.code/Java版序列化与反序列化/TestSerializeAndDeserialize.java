
/*
 * @Author: delevin.ying 
 * @Date: 2019-09-09 15:02:33 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-09-09 16:02:07
 */
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.io.FileOutputStream;
import java.io.FileInputStream;
import java.io.File;

public class TestSerializeAndDeserialize {
    public static void main(String[] args) throws Exception {
        SerializePerson();
        Person p = DeserializePerson();
        System.out.println("name={" + p.getName() + "},age={" + p.getAge() + "},sex={" + p.getSex() + "},{weight="+p.weight+"}");
    }

    public static void SerializePerson() throws FileNotFoundException, IOException {
        Person p = new Person();
        p.setAge(26);
        p.setName("delevin");
        p.setSex("man");
        p.weight = 120;
        ObjectOutputStream outPutS = new ObjectOutputStream(new FileOutputStream(new File("D:/Person.txt")));
        outPutS.writeObject(p);
        outPutS.close();
    }

    public static Person DeserializePerson() throws Exception, IOException {
        ObjectInputStream inPutS = new ObjectInputStream(new FileInputStream(new File("D:/Person.txt")));
        Person p = (Person) inPutS.readObject();
        return p;
    }
}

class Person implements Serializable {
    private static final long serialVersionUID = -5809782578272943999L;
    private int age;
    private String name;
    private String sex;

    public static int weight = 140;

    public int getAge() {
        return age;
    }

    public String getName() {
        return name;
    }

    public String getSex() {
        return sex;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }
}
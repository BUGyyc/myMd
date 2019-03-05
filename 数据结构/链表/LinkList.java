class Node<T>{
    Node<T> next;
    T data;
    public Node(T data){
        this.data = data;
    }

    public String toString(){
        return data.toString();
    }
}

public class LinkList<E>{
    private Node<E> head;
    private int size;
    public LinkList(){
        head = new Node<E>(null);
    }

    public Node<E> getHead(){
        return head;
    }

    public Node<E> add(E data,int index){
        return null;
    }
}
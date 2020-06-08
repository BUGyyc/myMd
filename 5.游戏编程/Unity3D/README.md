# Singleton
普通单例
```
public class ASingle{
    public static ASingle instance = null;
    public static ASingle(){
        if(instance == null){
            instance = new ASingle();
        }
        return instance;
    }
}
```

防止重复，直接用泛型
```
public interface ISingleton{
    void OnSingleton();
}

public static class SingletonCreator{
    <!-- public static T Creat -->
}
```
package my.design;

public class Application {
    public static void main(String[] args) {
        System.out.println("hello");
    }
}

/**
 * “饿汉式” 的单例实现方式
 * <p>
 * 可以保证线程安全
 */
class Direct {
    private final static Direct INSTANCE = new Direct();

    private Direct() {
    }

    public static Direct getInstance() {
        return INSTANCE;
    }
}

/**
 * 采用枚举类型的单例模式
 */
enum EnumDirector {
    INSTANCE;
    @Override
    public String toString() {
        return getDeclaringClass().getCanonicalName() + "@" + hashCode();
    }
}

/**
 * “懒汉式” 的单例模式
 * <p>
 * 内部类在被引用之前不会被类加载器加载，直到客户端调用的时候才被加载
 * <p>
 * 这个方案是线程安全的
 */
public final class LazyInitializationDirector {
    private LazyInitializationDirector() {

    }

    public static LazyInitializationDirector getStance() {
        return InstanceHolder.INSTANCE;
    }

    private static class InstanceHolder {
        private static final LazyInitializationDirector INSTANCE = new LazyInitializationDirector();
    }
}

/**
 * 线程安全、双检查锁的单例模式
 * <p>
 * volatile 所修饰的变量可以被看作是一种 “程度较轻的 synchronized ”； 与 synchronized 块相比，volatile
 * 变量所需的编码较少，并且运行时开销也较少， 但是它所能实现的功能也仅是 synchronized 的一部分。
 * <p>
 * 锁提供了两种主要特性：互斥（mutual exclusion） 和可见性（visibility）。
 * 互斥即一次只允许一个线程持有某个特定的锁，因此可使用该特性实现对共享数据的协
 * 调访问协议，这样，一次就只有一个线程能够使用该共享数据。可见性要更加复杂一些，
 * 它必须确保释放锁之前对共享数据做出的更改对于随后获得该锁的另一个线程是可见的
 */

public final class ThreadSafeDoubleCheckLocking {
    private static volatile ThreadSafeDoubleCheckLocking INSTANCE;

    private ThreadSafeDoubleCheckLocking() {
        // 防止通过反射进行实例化
        if (null != INSTANCE) {
            throw new IllegalStateException("该实例已经存在");
        }
    }

    public static ThreadSafeDoubleCheckLocking getInstance() {
        // 采用局部变量的形式可以提高约 25% 的性能
        ThreadSafeDoubleCheckLocking instance = INSTANCE;
        // 如果已经被实例化则直接返回该实例
        if (null == instance) {
            // 无法确定其他的线程是否已经完成初始化
            // 为了确保我们需要锁定一个对象来进行确认
            synchronized (ThreadSafeDoubleCheckLocking.class) {
                // 再次将实例分配给局部变量，检查它是否被其他线程初始化
                // 在当前线程被阻塞进入锁定区域时。如果它被初始化则直接返回之前创建的实例
                instance = INSTANCE;
                if (null == instance) {
                    INSTANCE = instance = new ThreadSafeDoubleCheckLocking();
                }
            }
        }
        return instance;
    }
}
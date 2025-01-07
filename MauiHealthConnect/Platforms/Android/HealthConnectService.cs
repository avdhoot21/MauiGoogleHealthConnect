using Android.Content;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Google.HealthConnect;
using System.Formats.Asn1;
namespace MauiHealthConnect.Platforms.Android
{
    [Register("com/google/healthconnect/HealthConnectWrapper")]
    internal class HealthConnectService : Java.Lang.Object
    {
        private static IntPtr _healthConnectWrapperClass;
        private static IntPtr _hasAllPermissionsMethod;
        private static IntPtr _readHeartRateDataMethod;
        private static IntPtr _readHeartRateDataAsyncMethod;
        IntPtr class_ref;
        public HealthConnectService(Context context) : base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
        {
            {
                class_ref = JNIEnv.FindClass("com/google/healthconnect/HealthConnectWrapper");
              //  _healthConnectWrapperClass = JNIEnv.FindClass("com/google/healthconnect/HealthConnectWrapperJNI");

                IntPtr ctor = JNIEnv.GetMethodID(class_ref, "<init>", "(Landroid/content/Context;)V");
                 SetHandle(JNIEnv.NewObject(class_ref, ctor, new JValue(context)), JniHandleOwnership.TransferLocalRef);
            }
        }
        public bool RequestPermissions(Context context)
        {
            
            IntPtr method_id = JNIEnv.GetMethodID(class_ref, "requestPermissions", "(Landroid/content/Context;)Z");
            return JNIEnv.CallBooleanMethod(Handle, method_id, new JValue(context));
        }

        // Methods for getting records
       /* public List<string> GetHeartRateRecords()
        {

            *//* IntPtr method_id = JNIEnv.GetMethodID(class_ref, "readHeartRateData", "()Ljava/util/List;");
             return (JavaList)JNIEnv.CallObjectMethod(Handle, method_id);*//*
            // Get the method ID for the non-suspend method
            
         // _healthConnectWrapperClass = JNIEnv.FindClass("com/google/healthconnect/HealthConnectWrapperJNI");

            // Get method signatures
            //_hasAllPermissionsMethod = JNIEnv.GetStaticMethodID(_healthConnectWrapperClass, "hasAllPermissions", "()Z");
            //_readHeartRateDataMethod = JNIEnv.GetStaticMethodID(_healthConnectWrapperClass, "readHeartRateData", "()Ljava/util/List;");
        //    _readHeartRateDataAsyncMethod = JNIEnv.GetStaticMethodID(_healthConnectWrapperClass, "readHeartRateDataAsync", "()Ljava/util/List;");
        
           // IntPtr class_ree = JNIEnv.FindClass("com/google/healthconnect/HealthConnectWrapper");

            IntPtr ctor = JNIEnv.GetMethodID(class_ref, "<init>", "(Landroid/content/Context;)V");
            // Step 3: Create an instance of the class
            IntPtr instance = JNIEnv.NewObject(class_ref, ctor);
            JavaList javaList = (JavaList)JNIEnv.CallObjectMethod(instance, JNIEnv.GetMethodID(class_ref, "readHeartRateDataAsync", "()Ljava/util/List;"));

            IntPtr method_id = JNIEnv.GetMethodID(class_ref, "readHeartRateDataAsync", "()Ljava/util/List;");
         //   JavaList javaList = (JavaList)JNIEnv.CallObjectMethod(Handle, method_id);
            return ConvertJavaListToDotNetList(javaList);
        }*/


        public List<string> GetHeartRateRecords()
{
    // Step 1: Find the Java class
    IntPtr class_ref = JNIEnv.FindClass("com/google/healthconnect/HealthConnectWrapper");

    if (class_ref == IntPtr.Zero)
    {
        throw new Exception("Class not found: com/google/healthconnect/HealthConnectWrapper");
    }

    // Step 2: Get the constructor method ID
    IntPtr constructorMethodId = JNIEnv.GetMethodID(class_ref, "<init>", "()V");

    if (constructorMethodId == IntPtr.Zero)
    {
        throw new Exception("Constructor not found for HealthConnectWrapper class.");
    }

    // Step 3: Create an instance of the class
    IntPtr instance = JNIEnv.NewObject(class_ref, constructorMethodId);

    if (instance == IntPtr.Zero)
    {
        throw new Exception("Failed to create an instance of HealthConnectWrapper.");
    }

    // Step 4: Get the method ID for the method you want to call
    IntPtr methodId = JNIEnv.GetMethodID(class_ref, "readHeartRateDataAsync", "()Ljava/util/List;");

    if (methodId == IntPtr.Zero)
    {
        throw new Exception("Method not found: readHeartRateDataAsync.");
    }

    // Step 5: Call the method on the instance
    JavaList javaList = (JavaList)JNIEnv.CallObjectMethod(instance, methodId);

    if (javaList == null)
    {
        throw new Exception("Received null from Java method.");
    }

    // Step 6: Convert the Java List to a C# List
    return ConvertJavaListToDotNetList(javaList);
}


        private List<string> ConvertJavaListToDotNetList(JavaList javaList)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < javaList.Size(); i++)
            {
                list.Add(javaList.Get(i).ToString());
            }
            return list;
        }
    }
}

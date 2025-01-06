using Android.Content;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Google.HealthConnect;
namespace MauiHealthConnect.Platforms.Android
{
    [Register("com/google/healthconnect/HealthConnectWrapper")]
    internal class HealthConnectService : Java.Lang.Object
    {
        IntPtr class_ref;
        public HealthConnectService(Context context) : base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
        {
            {
                class_ref = JNIEnv.FindClass("com/google/healthconnect/HealthConnectWrapper");
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
        public List<string> GetHeartRateRecords()
        {
            /* IntPtr method_id = JNIEnv.GetMethodID(class_ref, "readHeartRateData", "()Ljava/util/List;");
             return (JavaList)JNIEnv.CallObjectMethod(Handle, method_id);*/
            // Get the method ID for the non-suspend method
            IntPtr method_id = JNIEnv.GetMethodID(class_ref, "readHeartRateDataAsync", "()Ljava/util/List;");

            // Call the method
            JavaList javaList = (JavaList)JNIEnv.CallObjectMethod(Handle, method_id);

            // Convert the JavaList to .NET List
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

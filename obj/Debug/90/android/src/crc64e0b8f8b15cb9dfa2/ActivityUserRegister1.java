package crc64e0b8f8b15cb9dfa2;


public class ActivityUserRegister1
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("LicentaUTCN.ActivityUserRegister1, LicentaUTCN", ActivityUserRegister1.class, __md_methods);
	}


	public ActivityUserRegister1 ()
	{
		super ();
		if (getClass () == ActivityUserRegister1.class)
			mono.android.TypeManager.Activate ("LicentaUTCN.ActivityUserRegister1, LicentaUTCN", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

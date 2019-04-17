package md5138b61949e3f9b08b3a2cf4ee6f65b18;


public class MainActivity_CountDown
	extends android.os.CountDownTimer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTick:(J)V:GetOnTick_JHandler\n" +
			"n_onFinish:()V:GetOnFinishHandler\n" +
			"";
		mono.android.Runtime.register ("App.Droid.MainActivity+CountDown, App.Android", MainActivity_CountDown.class, __md_methods);
	}


	public MainActivity_CountDown (long p0, long p1)
	{
		super (p0, p1);
		if (getClass () == MainActivity_CountDown.class)
			mono.android.TypeManager.Activate ("App.Droid.MainActivity+CountDown, App.Android", "System.Int64, mscorlib:System.Int64, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public void onTick (long p0)
	{
		n_onTick (p0);
	}

	private native void n_onTick (long p0);


	public void onFinish ()
	{
		n_onFinish ();
	}

	private native void n_onFinish ();

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

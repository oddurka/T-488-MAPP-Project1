package md58a59fe7193c9e402f80a7a500404a919;


public class MovieListAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MovieSearch.Droid.MovieListAdapterViewHolder, MovieSearch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MovieListAdapterViewHolder.class, __md_methods);
	}


	public MovieListAdapterViewHolder ()
	{
		super ();
		if (getClass () == MovieListAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("MovieSearch.Droid.MovieListAdapterViewHolder, MovieSearch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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

using UnityEngine;

public class DontDistory : MonoBehaviour
{
	private static DontDistory thisGameObjectInstance;

	void Awake()
	{
		DontDestroyOnLoad(this);

		if (thisGameObjectInstance == null)
		{
			thisGameObjectInstance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
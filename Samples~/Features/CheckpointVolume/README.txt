To use this checkpoint volume, you will need the following:

1. The volume placed in a scene

2. A data holder script (for a checkpoint you will need a small script which has the x, y, z position stored)

3. A script which loads the saved data into the game. This could be done for instance with a gamemanager. (this is not included with this package)

Extra Info:
When you save something with this volume, it will be stored in the project's persisten data path. You can check the exact path location with:
Debug.Log(Application.persistentDataPath);

this will return the persistent data path of your project, which also includes the save(d) file(s)

If you want to load the data you saved, you can do so with LuckyGamezLib.SaveSystem.LoadSavedDataFromFile(TheFileNameYouSavedTheFileWith);

Notes:
One of the two objects will need a rigidbody for it to detect the collision (this is always the case in unity). This can be either achieved
by putting a rigidbody on the volume itself, or the object that will walk into it. For the example I've put the collider on the volume
to make it compatible with all projects by default.

This example features the use of tags to detect the objects. This is done to make it as compatible as possible with all projects.
It is recomended that you change the detection method that's best suitable for you project's needs.



# AnimatorParameterReference

Avoid string references for long term project! For animator parameter names, you can use scriptable object instead. Hash is also cached :). 

Why:
- Use scriptable reference instead of string in code
- Object field in inspector. Picker shows the list of existing parameters (really conveniant)
- If you change name in animator you just have to update the related scriptable. No longer need to fix all scripts in every scenes 😱


And I added an editor script to generate scriptables from animator. It only works for first initialization (too much complicated to handle renamed parameters, high risk to break all references to scriptable). Right click on a animator controller asset then "Animator/Generate Parameters References".




## Further details

The class AnimatorParameterReference is a subclass of ScriptableObject. It means that we can create ScriptableObject of types AnimatorParameterReference. ScriptableObject are assets displayed in the project window.

For each animator controller, you can create one AnimatorParameterReference instance per parameter. Giving for each an explicit asset name and the correct parameter name in the field ParameterName (displayed in inspector). 

Note: we need an internal string field instead of using directly asset name because some characters (like '/') can't be used in filename.

AnimatorParameterReference acts like a wrapper. In every part of code which need to get or set an animator parameter, use AnimatorParameterReference parameter instead of string parameter

![Image of Yaktocat](https://pbs.twimg.com/media/DnsrlqNXgAAWMdT.jpg)

Using SerializeField, my variable is displayed in inspector, so anybody can define the parameter. Can be useful for more generic scripts (a script SetAnimatorFloatParameter for example). For a given scriptable object, I can find all references in scene too.

![Image of Yaktocat](https://pbs.twimg.com/media/DnsrrYMXgAA5aOc.jpg)

Hash is entirely managed in AnimatorParameterReference code. It's displayed in inspector only for debug purpose. When you get or set animtor parameter, it's better to use hash of the name instead of name directly. This is done automatically when you use AnimatorParameterReference




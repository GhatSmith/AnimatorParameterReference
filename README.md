# AnimatorParameterReference

Avoid string references for long term project! For animator parameter names, you can use scriptable object instead. Hash is also cached :). 

Why:
- Use scriptable reference instead of string in code
- Object field in inspector. Picker shows the list of existing parameters (really conveniant)
- If you change name in animator you just have to update the related scriptable. No longer need to fix all scripts in every scenes 😱


And I added an editor script to generate scriptables from animator. It only works for first initialization (too much complicated to handle renamed parameters, high risk to break all references to scriptable). Right click on a animator controller asset then "Animator/Generate Parameters References".

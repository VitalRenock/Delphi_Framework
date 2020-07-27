Permet d'implémenter une "State Machine" à un gameObject ainsi que ses divers "State".
Fonctionne en combinaison de "l'Animator".

Requiert un composant Animator sur le gameObject.
Requiert un "Animator Controller"
	Dans Assets: Create/Animator Controller.
Requiert une animation par état souhaité ainsi que son/ses "StateMachineBehaviour" associés.
	Pour une nouvelle animation:
	- Dans Assets: Create/Animation
	- Glisser l'animation dans "l'Animator Controller" du gameObject (qui sera donc un nouvel état).
	Pour ajouter un nouveau "StateMachineBehaviour" à une animation:
	- Sélectionné l'animation depuis "l'Animator Controller" et dans l'inspector,
	cliqué sur "Add Behaviour" qui créera un nouveau script.
	- Scripter le comportement voulus.

Requiert un nouveau "State Machine Controller":
- Créer un nouveau script héritant de "SM_BaseAnimatorController".
- Implémenter les classes abtraites demandée par la classe.
- Si besoin de "Awake()" ou "Update()", "overridé" celles du parent.
	Donc:	- protected override void Awake()
			base.Awake();
		- protected override void Update()
			base.Update();
- Ensuite, on crée les variables requises pour changer de "State" 
	ex: float distanceJoueur
- Dans CheckTransitions(), on mets à jour la valeur de nos variables.
	ex: distanceJoueur = Vector3.Distance(transform.position, Player.position);
- Dans UpdateTransitions(), on mets à jour les variables de l'animator créer.
	ex: if (!Animator.GetBool("PlayerIsNear") && distancePlayer <= 10f)
			Animator.SetBool("PlayerIsNear", true);
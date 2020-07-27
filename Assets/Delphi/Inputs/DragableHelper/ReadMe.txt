Permet d'appeller des méthodes customs quand la souris "Drag" l'objet.

- Placer en tant que composant "Draggable" sur l'objet pour qu'il soit apte.
- Via un autre script ajouter des méthodes customs à une des trois "UnityAction" disponibles.
	"UnityAction" disponibles:
	- BeginDragAction
	- DragAction
	- EndDragAction
- Attention, les méthodes customs crées doivent absolument avoir la même signature que les "UnityAction"
	signature = "void CustomMethode(PointerEventData eventData)"
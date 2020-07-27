Permet d'appeller des méthodes customs quand la souris "Pointe" l'objet.

- Placer en tant que composant "Pointable" sur l'objet pour qu'il soit apte.
- Via un autre script (ou par l'inspector) ajouter des méthodes customs à un des dix "UnityEvent" disponibles.
	"UnityEvent" disponibles:
	- onPointerEnter
	- onPointerEnterEventData
	- onPointerClick
	- onPointerClickEventdata
	- onPointerExit
	- onPointerExitEventData
	- onPointerDown
	- onPointerDownEventData
	- onPointerUp
	- onPointerUpEventData

- Attention, les méthodes customs crées doivent absolument avoir la même signature que les "UnityEvent" choisis.
	signature disponibles =	"void CustomMethode()"
				"void CustomMethode(PointerEventData eventData)"
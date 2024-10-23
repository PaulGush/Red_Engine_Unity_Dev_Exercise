using UnityEngine;

namespace RedEngine
{
	public class Puck : MonoBehaviour
	{
		[SerializeField] private MeshRenderer colourRenderer;
		[SerializeField, ColorUsage(false, true)] private Color blueColour;
		[SerializeField, ColorUsage(false, true)] private Color pinkColour;
		
		public TeamColour TeamColour { get; private set; }

		private static MaterialPropertyBlock _propertyBlock;
		
		private void Awake()
		{
			if (_propertyBlock == null)
			{
				_propertyBlock = new MaterialPropertyBlock();
			}
		}
		
		public void SetTeamColour(TeamColour colour)
		{
			TeamColour = colour;

			SetPropertyBlock();
		}

		private void SetPropertyBlock()
		{
			colourRenderer.GetPropertyBlock(_propertyBlock);

			var colour = TeamColour == TeamColour.Blue ? blueColour : pinkColour;
			_propertyBlock.SetColor("_BaseColor", colour);

			colourRenderer.SetPropertyBlock(_propertyBlock);
		}
	}
}
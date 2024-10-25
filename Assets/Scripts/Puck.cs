using System;
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
		
		public static Action<Puck> OnAnyTeamColourChanged;
		public static Action<Puck> OnAnyStatusChanged;
		
		private void Awake()
		{
			if (_propertyBlock == null)
			{
				_propertyBlock = new MaterialPropertyBlock();
			}
		}

		private void OnEnable()
		{
			OnAnyStatusChanged?.Invoke(this);
		}
		
		private void OnDisable()
		{
			OnAnyStatusChanged?.Invoke(this);
		}

		private void OnDestroy()
		{
			OnAnyStatusChanged?.Invoke(this);
		}
		public void SetTeamColour(TeamColour colour)
		{
			TeamColour = colour;
			
			OnAnyTeamColourChanged?.Invoke(this);
			
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
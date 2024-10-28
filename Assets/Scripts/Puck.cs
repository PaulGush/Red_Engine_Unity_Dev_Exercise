using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RedEngine
{
	[SelectionBase]
	public class Puck : MonoBehaviour
	{
		public LookAtTarget InboundArcTargetSocket;
		public LookAtTarget OutboundArcTargetSocket;
		
		[SerializeField] private MeshRenderer colourRenderer;
		[SerializeField, ColorUsage(false, true)] private Color blueColour;
		[SerializeField, ColorUsage(false, true)] private Color pinkColour;
		
		public TeamColour TeamColour { get; private set; }
		private int teamColourIndex = -1;
		
		private static MaterialPropertyBlock _propertyBlock;
		
		public static Action<Puck> OnAnyStatusChanged;
		
		private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

		private void Awake()
		{
			if (_propertyBlock == null)
			{
				_propertyBlock = new MaterialPropertyBlock();
			}
		}

		private void OnEnable()
		{
			if (teamColourIndex > -1)
			{
				OnAnyStatusChanged?.Invoke(this);
			}
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
			teamColourIndex = (int)colour;
			
			OnAnyStatusChanged?.Invoke(this);
			
			SetPropertyBlock();
		}

		private void SetPropertyBlock()
		{
			colourRenderer.GetPropertyBlock(_propertyBlock);

			var colour = TeamColour == TeamColour.Blue ? blueColour : pinkColour;
			_propertyBlock.SetColor(BaseColor, colour);

			colourRenderer.SetPropertyBlock(_propertyBlock);
		}
	}
}
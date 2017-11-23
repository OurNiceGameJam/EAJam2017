using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 100;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName1 = "Horizontal1"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName1 = "Vertical1"; // The name given to the vertical axis for the cross platform input
        public string horizontalAxisName2 = "Horizontal2"; // The name given to the horizontal axis for the cross platform input
        public string verticalAxisName2 = "Vertical2"; // The name given to the vertical axis for the cross platform input

        Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis1; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis1; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis2; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis2; // Reference to the joystick in the cross platform input

        void OnEnable()
		{
			CreateVirtualAxes();
		}

        void Start()
        {
            m_StartPos = transform.position;
        }

		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis1.Update(-delta.x);
                m_HorizontalVirtualAxis2.Update(-delta.x);
            }

			if (m_UseY)
			{
				m_VerticalVirtualAxis1.Update(delta.y);
                m_VerticalVirtualAxis2.Update(delta.y);
            }
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis1 = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName1);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis1);
                m_HorizontalVirtualAxis2 = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName2);
                CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis2);
            }
			if (m_UseY)
			{
				m_VerticalVirtualAxis1 = new CrossPlatformInputManager.VirtualAxis(verticalAxisName1);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis1);
                m_VerticalVirtualAxis2 = new CrossPlatformInputManager.VirtualAxis(verticalAxisName2);
                CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis2);
            }
		}


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;

			if (m_UseX)
			{
				int delta = (int)(data.position.x - m_StartPos.x);
				delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (m_UseY)
			{
				int delta = (int)(data.position.y - m_StartPos.y);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
			transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
			UpdateVirtualAxes(transform.position);
		}


		public void OnPointerUp(PointerEventData data)
		{
			transform.position = m_StartPos;
			UpdateVirtualAxes(m_StartPos);
		}


		public void OnPointerDown(PointerEventData data) { }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis1.Remove();
                m_HorizontalVirtualAxis2.Remove();
            }
			if (m_UseY)
			{
				m_VerticalVirtualAxis1.Remove();
                m_VerticalVirtualAxis2.Remove();
            }
		}
	}
}
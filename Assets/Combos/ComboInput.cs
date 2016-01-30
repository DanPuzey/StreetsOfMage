using System;
using UnityEngine;

namespace WizardDuel
{
    public class ComboInput : MonoBehaviour
    {
        #region Inspector fields
        public string ButtonPrefix = "joystick 1 ";
        public string AxisPrefix = "Joy 1 ";
        public string[] InputButtons =
        {
            "button 0",
            "button 1",
            "button 2",
            "button 3",
            null,
            null,
            null,
            null
        };
        public AxisInfo[] InputAxes =
        {
            null,
            null,
            null,
            null,
            new AxisInfo("Axis 7", true),
            new AxisInfo("Axis 7", false),
            new AxisInfo("Axis 6", false),
            new AxisInfo("Axis 6", true)
        };
        public float AxisDeadzone = 0.2f;

        public GameObject TargetRoot;
        #endregion

        private int _controlCount;
        private string[] _buttons;
        private AxisInfo[] _axes;

        private void Awake()
        {
            _controlCount = InputButtons.Length;
            _buttons = new string[_controlCount];
            _axes = new AxisInfo[_controlCount];
            
            for (var i = 0; i < _controlCount; i++)
            {
                if (!string.IsNullOrEmpty(InputButtons[i]))
                {
                    _buttons[i] = string.Concat(ButtonPrefix, InputButtons[i]);
                }

                if (InputAxes[i] != null && !string.IsNullOrEmpty(InputAxes[i].Name))
                {
                    var axisName = string.Concat(AxisPrefix, InputAxes[i].Name);
                    _axes[i] = new AxisInfo(axisName, InputAxes[i].Positive);
                }
            }
        }

        private void Update()
        {
            for (var i = 0; i < InputButtons.Length; i++)
            {
                if (_buttons[i] != null && Input.GetKeyDown(_buttons[i]))
                {
                    SendGlyph(i);
                    return;
                }

                AxisInfo axis = _axes[i];

                if (axis != null && !string.IsNullOrEmpty(axis.Name))
                {
                    var axisValue = Input.GetAxis(axis.Name);

                    if (Mathf.Abs(axisValue) > AxisDeadzone)
                    {
                        if (!axis.IsPressed)
                        {
                            if (axis.Positive && axisValue > 0)
                            {
                                SendGlyph(i);
                                axis.IsPressed = true;
                                return;
                            }
                            else if (!axis.Positive && axisValue < 0)
                            {
                                SendGlyph(i);
                                axis.IsPressed = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        axis.IsPressed = false;
                    }
                }
            }
        }

        private void SendGlyph(int index)
        {
            TargetRoot.BroadcastMessage("AddGlyph", index);
        }

        [Serializable]
        public class AxisInfo
        {
            public string Name;
            public bool Positive;

            public AxisInfo(string name, bool positive)
            {
                Name = name;
                Positive = positive;
            }

            public bool IsPressed { get; set; }
        }
    }
}

using DiagramToolkit.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramToolkit
{
    class PolygonDrawState : DrawingState
    {
        private static DrawingState instance;

        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new PolygonDrawState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.RenderOnPreview();
        }

        public override void Deselect(DrawingObject obj)
        {
            obj.ChangeState(StaticState.GetInstance());
        }
        public override void Select(DrawingObject obj)
        {
            obj.ChangeState(PolygonDrawState.GetInstance());
        }

    }
}

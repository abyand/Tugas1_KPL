using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramToolkit.Api.States
{
    class PolygonState : DrawingState
    {
        private static DrawingState instance;

        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new PolygonState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.RenderOnPreview();
        }

        public override void Deselect(DrawingObject obj)
        {
            obj.ChangeState(PolygonState.GetInstance());
        }
    }
}

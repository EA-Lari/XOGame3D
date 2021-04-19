using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    public class GameController
    {
        public void SetState(ICell cell, States state)
        {
            cell.State = state;
            var area = cell.ParentArea;
            area.CurrentCell = cell;
            if (area.State != States.Empty) return;

            var checker = new AreaChecked(area);
            var newState = checker.CheckByState(cell.State);
            if (newState != States.Empty)
                if(area is ICell cellArea)
                    SetState(cellArea, newState);
        }
    }
}

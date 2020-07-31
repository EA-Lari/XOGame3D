
namespace XOGame3D.Logic
{
    internal class PlayField : Area<PartField>
    {
        public PartField NextField { get; set; }

        public override void SetState(PartField cell)
        {
            base.SetState(cell);
            NextField = cell;
        }
    }
}

using RGeos.Geometries;

namespace RGeos.Desktop.ControlCommands
{
    public interface IPickUpPoint
    {
        RgPoint Point { get; set; }
        event PickUpFinished PickUpFinishedEventHandler;
    }
    public delegate void PickUpFinished(RgPoint p1);
}

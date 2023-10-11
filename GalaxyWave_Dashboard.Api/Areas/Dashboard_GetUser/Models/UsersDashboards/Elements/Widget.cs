namespace GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards.Elements
{
    public class Widget
    {
        public Guid WidgetId { get; set; }
        public Dashboard dashboard { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

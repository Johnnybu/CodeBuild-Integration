namespace NightlyBuilds.Model
{
    public class Input
    {
        public Input()
        {
            BuildSpec = "buildspec.yml";
        }

        public string ProjectName { get; set; }

        public string BuildSpec { get; set; }
    }
}

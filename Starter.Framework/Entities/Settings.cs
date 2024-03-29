﻿namespace Starter.Framework.Entities
{
    /// <summary>
    /// Implements base application settings
    /// </summary>
    public class Settings : ISettings
    {
        public virtual string ApiUrl => "https://dev-astra-boyankostadinov-starter-api.azurewebsites.net";

        public virtual string ApplicationInsightsInstrumentationKey => "0892a77e-7112-46a3-a207-938992438722";

        public virtual string CatEntityTableName => "Cats";

        public virtual string TableStorageConnectionString =>
            "DefaultEndpointsProtocol=https;AccountName=devastraboyankostadinov;AccountKey=Tt+oWyP1oBSYOgFGLqonhOVEtAVp+q2GaCMe32IZR0AY9fL87PjofMnILJpyUMRJM8+LTgyM/FQ82LyUahWd1Q==;EndpointSuffix=core.windows.net";
        
        public virtual string ResourceUrl => "/cat";
        
        public virtual string ServiceBusConnectionString => "Endpoint=sb://dev-astra-boyankostadinov-starter.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5S5FVbpHqzbt1qBp/0KR2HamtBCITTdJAPv9ExngqtE=";

        public virtual string ServiceBusQueue => "starter.queue";

    }
}
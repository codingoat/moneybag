/*namespace ZUtils
{
    public static class FmodUtils
    {
        public static void SetParameter(this EventInstance instance, FmodParameter parameter)
        {
            var result = parameter.HasValue
                ? instance.setParameterByName(parameter.name, parameter.Value)
                : instance.setParameterByNameWithLabel(parameter.name, parameter.ValueLabel);
            
            if (result != RESULT.OK)
            {
                instance.getDescription(out EventDescription eventDesc);
                eventDesc.getPath(out string eventName);
                UnityEngine.Debug.LogError($"FMOD Parameter error {result}! Event: {eventName}, Parameter: {parameter}");
            }
            // var aa = RuntimeManager.StudioSystem.setParameterByNameWithLabel(name, label); // for global params
        }
        
        public static void Play(this EventReference fmodEvent, params FmodParameter[] parameters)
        {
            EventInstance instance = RuntimeManager.CreateInstance(fmodEvent);

            foreach (FmodParameter parameter in parameters) 
                instance.SetParameter(parameter);
            
            instance.start();
            instance.release();
        }
    }
}*/
using System;

namespace NEO.NEOColorPicker {

    [Serializable]
    public enum Model {
        RGB,
        HSL,
        HSV
    }

    public static class ColorModelMethods {

        public static Model Next(this Model model) {
            Model[] models = (Model[])Enum.GetValues(typeof(Model));
            int sucessor = Array.IndexOf(models, model) + 1;
            return (models.Length == sucessor) ? models[0] : models[sucessor];
        }

    }
}

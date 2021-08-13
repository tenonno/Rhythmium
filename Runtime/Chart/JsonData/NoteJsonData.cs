using System;
using Guid = System.String;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public struct NoteCustomPropsJsonData
    {
        public string type;
        public Guid targetNoteLine;
        public float scale;
        public float distance;
    }

    [Serializable]
    public sealed class NoteJsonData
    {
        public Guid guid;
        public int horizontalSize;
        public FractionJsonData horizontalPosition;
        public int measureIndex;
        public FractionJsonData measurePosition;
        public string type;
        public Guid lane;
        public NoteCustomPropsJsonData customProps;

        public override string ToString()
        {
            return
                $"{type} m({measureIndex + measurePosition.To01()}) h({horizontalPosition.Denominator}/{horizontalPosition.Denominator}) s({horizontalSize}) {guid}";
        }

        public NoteJsonData Mirror()
        {
            var mirroredNumerator =
                horizontalPosition.Denominator - 1 - horizontalPosition.Numerator - (horizontalSize - 1);
            var mirroredHorizontalPosition = new FractionJsonData(mirroredNumerator, horizontalPosition.Denominator);

            return new NoteJsonData
            {
                guid = guid,
                horizontalSize = horizontalSize,
                horizontalPosition = mirroredHorizontalPosition,
                measureIndex = measureIndex,
                measurePosition = measurePosition,
                type = type,
                lane = lane,
                customProps = customProps
            };
        }
    }
}
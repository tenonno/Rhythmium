using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class NoteJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private string guid;
        [SerializeField] private int horizontalSize;
        [SerializeField] private FractionJsonData horizontalPosition;
        [SerializeField] private int measureIndex;
        [SerializeField] private FractionJsonData measurePosition;
        [SerializeField] private string type;
        [SerializeField] private string lane;
        [SerializeField] private string layer;
        [SerializeField] private NoteCustomPropsJsonData customProps;

        // ReSharper restore InconsistentNaming

        public string Guid => guid;
        public int HorizontalSize => horizontalSize;
        public FractionJsonData HorizontalPosition => horizontalPosition;
        public int MeasureIndex => measureIndex;
        public FractionJsonData MeasurePosition => measurePosition;
        public string Type => type;
        public string Lane => lane;
        public string Layer => layer;
        public NoteCustomPropsJsonData CustomProps => customProps;

        public NoteJsonData(
            string guid,
            int horizontalSize,
            FractionJsonData horizontalPosition,
            int measureIndex,
            FractionJsonData measurePosition,
            string type,
            string lane,
            string layer,
            NoteCustomPropsJsonData customProps
        )
        {
            this.guid = guid;
            this.horizontalSize = horizontalSize;
            this.horizontalPosition = horizontalPosition;
            this.measureIndex = measureIndex;
            this.measurePosition = measurePosition;
            this.type = type;
            this.lane = lane;
            this.layer = layer;
            this.customProps = customProps;
        }

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

            return new NoteJsonData(
                guid,
                horizontalSize,
                mirroredHorizontalPosition,
                measureIndex,
                measurePosition,
                type,
                lane,
                layer,
                customProps
            );
        }

        public NoteJsonData WithType(string newType)
        {
            return new NoteJsonData(
                guid,
                horizontalSize,
                horizontalPosition,
                measureIndex,
                measurePosition,
                newType,
                lane,
                layer,
                customProps
            );
        }
    }
}
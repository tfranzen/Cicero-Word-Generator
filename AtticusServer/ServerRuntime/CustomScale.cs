using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

using NationalInstruments.DAQmx;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class CustomScale
    {
        private string physChannel;

        [Description("Name of Physical Channel.")]
        public string PhysicalChannel
        {
            get { return physChannel; }
            set { physChannel = value; }
        }
        private string scaleName;

        [Description("Name of scale.")]
        public string Scale
        {
            get { return scaleName; }
            set { scaleName = value; }
        }

        public double getMax()
        {
            Scale niscale = DaqSystem.Local.LoadScale(scaleName);
            if (niscale.Type == ScaleType.Linear)
            {
                double m = ((LinearScale)niscale).Slope;
                double n = ((LinearScale)niscale).YIntercept;
                return n + 10.0 * Math.Abs(m);
            }
            else if (niscale.Type == ScaleType.Table)
            {
                return ((TableScale)niscale).ScaledValues.Max();               
            }
            else if (niscale.Type == ScaleType.RangeMap)
            {
                return ((RangeMapScale)niscale).ScaledMax;
            }
            else return 10.0;
        }

        public double getMin()
        {
            Scale niscale = DaqSystem.Local.LoadScale(scaleName);
            if (niscale.Type == ScaleType.Linear)
            {
                double m = ((LinearScale)niscale).Slope;
                double n = ((LinearScale)niscale).YIntercept;
                return n + -10.0 * Math.Abs(m);
            }
            else if (niscale.Type == ScaleType.Table)
            {
                return ((TableScale)niscale).ScaledValues.Min();
            }
            else if (niscale.Type == ScaleType.RangeMap)
            {
                return ((RangeMapScale)niscale).ScaledMin;
            }
            else return 10.0;
        }

        public CustomScale()
        {
            physChannel = "";
            scaleName = "";
        }
    }
}

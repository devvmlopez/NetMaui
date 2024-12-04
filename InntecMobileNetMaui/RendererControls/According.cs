using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InntecMobileNetMaui.RendererControls
{
    public class According : Frame
    {
        public bool IsOpen = false;

        /// <summary>
        /// animate an object
        /// </summary>
        /// <param name="property">Properti to animated</param>
        /// <param name="start">start the property value</param>
        /// <param name="end">end the property valye</param>
        /// <param name="time">time of miliseconds</param>
        public void animateOut(string property, double start, double end, uint time)
        {
            if (!IsOpen)
                this.Animate(property,
                    new Animation((double d) => { this.HeightRequest = d; },
                    start: start,
                    end: end,
                    easing: Easing.SpringIn),
                    length: time);

            IsOpen = true;
        }
        /// <summary>
        /// animate an object
        /// </summary>
        /// <param name="property">Properti to animated</param>
        /// <param name="start">start the property value</param>
        /// <param name="end">end the property valye</param>
        /// <param name="time">time of miliseconds</param>
        public void animateIn(string property, double start, double end, uint time)
        {
            if (IsOpen)
                this.Animate(property,
                    new Animation((double d) => { this.HeightRequest = d; },
                    start: start,
                    end: end,
                    easing: Easing.SpringIn),
                    length: time);

            IsOpen = false;
        }


    }
}

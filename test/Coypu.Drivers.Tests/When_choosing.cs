﻿using Shouldly;
using Xunit;

namespace Coypu.Drivers.Tests
{
    public class When_choosing : DriverSpecs
    {
        [Fact]
        public void Chooses_radio_button_from_list()
        {
            var radioButton1 = Field("chooseRadio1");
            radioButton1.Selected.ShouldBeFalse();

            // Choose 1
            Driver.Choose(radioButton1);

            var radioButton2 = Field("chooseRadio2");
            radioButton2.Selected.ShouldBeFalse();

            // Choose 2
            Driver.Choose(radioButton2);

            // New choice is now selected
            radioButton2 = Field("chooseRadio2");
            radioButton2.Selected.ShouldBeTrue();

            // Originally selected is no longer selected
            radioButton1 = Field("chooseRadio1");
            radioButton1.Selected.ShouldBeFalse();
        }


        [Fact]
        public void Fires_onclick_event()
        {
            var radio = Field("chooseRadio2");
            radio.Value.ShouldBe("Radio buttons - 2nd value");

            Driver.Choose(radio);

            Field("chooseRadio2", Root).Value.ShouldBe("Radio buttons - 2nd value - clicked");
        }
    }
}
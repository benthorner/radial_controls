# RadialControls #

Circular controls library for Windows 8 Apps.

  * Chains - for circular text
  * Sliders - for time pickers
  * Arcs - for doughnut charts
  * Slices - for pie charts

The library also contains some ready-made examples.

## Installation ##

TODO

## Usage ##

There are 3 main types of control (hardest first).

  * Halo - A halo is a panel which can be used to manage nested rings or bands of controls, such as the hour and minute sliders on a time picker or clock

  * HaloRing / HaloChain - A ring can be used to position items around a circle; a chain is a specialised ring used to display circular text

  * HaloArc / HaloSlice - An arc is a circular path, like a slice of doughnut; a slice is a filled arc, like a slice of pie

The Slider control can also be used inside a Halo.

### Halo ###

Draw three concentric bands (red, green and blue). The Halo will occupy all the space given to it.

    <control:Halo>
        <control:HaloArc StrokeThickness="30" Stroke="Red" control:Halo.Band="3"/>
        <control:HaloArc StrokeThickness="30" Stroke="Green" control:Halo.Band="2"/>
        <control:HaloSlice Stroke="Blue"/>
    </control:Halo>

Any children which are not in a band will be placed in the centre of the halo. Controls can share a band.

### HaloRing ###

Draw four dots in a circle (0, 90, 180, 270 degrees). The dots will be rotated around the centre of the ring.

    <control:HaloRing>
        <Ellipse Width="30" Height="30" Fill="White"/>
        <Ellipse Width="30" Height="30" Fill="White" control:HaloRing.Angle="90"/>
        <Ellipse Width="30" Height="30" Fill="White" control:HaloRing.Angle="180"/>
        <Ellipse Width="30" Height="30" Fill="White" control:HaloRing.Angle="270"/>
    </control:HaloRing>

Alternatively, you can use the Offset property, which will place the child at a point on the circle, without rotating it.

### HaloChain ###

Draw a circular label, centred at 0 degrees. Take a look at Examples/RingLabel for an out-of-the-box label control.

    <control:HaloChain Tension="0.5" Spacing="10">
        <TextBlock Text="H" FontSize="30"/>
        <TextBlock Text="E" FontSize="30"/>
        <TextBlock Text="L" FontSize="30"/>
        <TextBlock Text="L" FontSize="30"/>
        <TextBlock Text="O" FontSize="30"/>
    </control:HaloChain>

The HaloChain also has Angle and Offset properties, which behave the same as for HaloRing, but apply to the chain as a whole.

### HaloArc ###

Draw two half-rings (white and black), centred at 0 and 180 degrees. I've assumed the arcs are inside a Grid or Halo panel.

    <control:HaloArc Spread="180" Tension="0.5" Stroke="White" StrokeThickness="30"/>
    <control:HaloArc Angle="180" Spread="180" Tension="0.5" Stroke="Black" StrokeThickness="30"/>

The HaloArc also has an Offset property, which is added to the Angle to determine where to start drawing.

### HaloSlice ###

Draw two semi-circles (white and black), where the line of symmetry is vertical. Again, I assume an appropriate parent panel.

    <control:HaloSlice Offset="90" Spread="180" Fill="White"/>
    <control:HaloSlice Offset="90" Angle="180" Spread="180" Fill="White"/>

In general, Offset means origin - where to start drawing, and Angle means a rotation about the centre, starting from the Offset.

### Slider ###

Draw a slider with a rectangular thumb. Take a look at Examples/ArcSlider for a slicker version of this control.

    <control:Slider Angle="{Binding SomeProperty}">
       <control:Slider.Thumb>
          <ControlTemplate>
             <Rectangle Width="30" Height="30" Fill="White"/>
          </ControlTemplate>
       </control:Slider.Thumb>
    </control:Slider>

The Slider control also has an Offset property to set where the zero Angle should be (vertical up by default).
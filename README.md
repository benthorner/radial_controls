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
        <control:HaloSlice Stroke="Blue">
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

Draw a circular label, centred at 0 degrees. Take a look at the RingLabel user control for an out-of-the-box label control.

    <control:HaloChain Tension="0.5" Spacing="10">
        <TextBlock Text="H" FontSize="30"/>
        <TextBlock Text="E" FontSize="30"/>
        <TextBlock Text="L" FontSize="30"/>
        <TextBlock Text="L" FontSize="30"/>
        <TextBlock Text="O" FontSize="30"/>
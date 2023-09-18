# Description
The Publish-Subscribe Pattern, often abbreviated as Pub-Sub, is a behavioral design pattern used in software development to establish communication between multiple components or objects without them having to explicitly reference each other. This pattern is widely used in various programming languages, including C#, to implement loosely coupled, event-driven architectures. It is especially useful in scenarios where multiple components need to react to events or changes in the system.

Here's an explanation of the Publish-Subscribe Pattern in C#:

1. **Publisher**: The publisher is responsible for generating events or messages. In C#, this can be any object or component that raises events. These events represent something of interest that has occurred or changed.

2. **Subscriber**: Subscribers are the components or objects that are interested in receiving and reacting to events or messages. They subscribe to specific events or types of messages that they want to be notified about.

3. **Event/Messaging System**: The heart of the Publish-Subscribe Pattern is the event/messaging system that acts as an intermediary between publishers and subscribers. In C#, this system is typically implemented using events and delegates for simplicity. Delegates allow you to define and invoke methods dynamically at runtime.

# Step-by-step Code
Here's a step-by-step guide to implementing the Publish-Subscribe Pattern in C#:

1. **Define an Event**: In the publisher class, declare an event using the `event` keyword. This event will be the mechanism through which subscribers can listen to specific occurrences.

    ```csharp
    public class Publisher
    {
        public event EventHandler<EventArgs> SomeEvent;
        
        // Other methods and logic...
    }
    ```

2. **Raise the Event**: In the publisher class, when the event of interest occurs, raise the event. This notifies all subscribers that something has happened.

    ```csharp
    public void DoSomething()
    {
        // Do something...
        
        // Raise the event
        SomeEvent?.Invoke(this, EventArgs.Empty);
    }
    ```

3. **Subscribe to the Event**: In the subscriber class, subscribe to the event by adding an event handler method that will be executed when the event is raised.

    ```csharp
    public class Subscriber
    {
        public void Subscribe(Publisher publisher)
        {
            publisher.SomeEvent += HandleEvent;
        }
        
        private void HandleEvent(object sender, EventArgs e)
        {
            // React to the event here
        }
    }
    ```

4. **Unsubscribe (Optional)**: To unsubscribe from the event, the subscriber can remove its event handler.

    ```csharp
    public void Unsubscribe(Publisher publisher)
    {
        publisher.SomeEvent -= HandleEvent;
    }
    ```

By following this pattern, publishers and subscribers are decoupled. Publishers don't need to know who their subscribers are, and subscribers don't need to have direct references to publishers. This makes the code more maintainable and flexible, as new subscribers can be added or removed without impacting the existing codebase. The Publish-Subscribe Pattern is particularly useful in scenarios such as GUI frameworks, message processing systems, and real-time applications where multiple components need to react to events and messages asynchronously.


# Real world scenario
A real-world scenario can be for example a weather monitoring system. Imagine you're building a weather monitoring application where multiple weather sensors (publishers) provide real-time weather data, and various displays (subscribers) want to receive updates about temperature changes. The displays could include a console application, a graphical user interface (GUI), and a web-based dashboard.

Here's a basic implementation using C#:

```csharp
using System;
using System.Collections.Generic;

// Publisher: WeatherSensor
public class WeatherSensor
{
    // Event to publish temperature updates
    public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

    // Simulated temperature reading
    private double temperature;

    public double Temperature
    {
        get { return temperature; }
        set
        {
            if (temperature != value)
            {
                temperature = value;
                OnTemperatureChanged(new TemperatureChangedEventArgs(temperature));
            }
        }
    }

    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }
}

// Event argument for temperature changes
public class TemperatureChangedEventArgs : EventArgs
{
    public double NewTemperature { get; }

    public TemperatureChangedEventArgs(double newTemperature)
    {
        NewTemperature = newTemperature;
    }
}

// Subscriber: ConsoleDisplay
public class ConsoleDisplay
{
    public void Subscribe(WeatherSensor sensor)
    {
        sensor.TemperatureChanged += HandleTemperatureChange;
    }

    public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Console Display: Temperature changed to {e.NewTemperature}°C");
    }
}

// Subscriber: GuiDisplay
public class GuiDisplay
{
    public void Subscribe(WeatherSensor sensor)
    {
        sensor.TemperatureChanged += HandleTemperatureChange;
    }

    public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
    {
        // Update the GUI with the new temperature
        // This is where you would update a graphical interface
        Console.WriteLine($"GUI Display: Temperature changed to {e.NewTemperature}°C");
    }
}

// Subscriber: WebDashboard
public class WebDashboard
{
    public void Subscribe(WeatherSensor sensor)
    {
        sensor.TemperatureChanged += HandleTemperatureChange;
    }

    public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
    {
        // Update the web-based dashboard with the new temperature
        // This is where you would send updates to a web application
        Console.WriteLine($"Web Dashboard: Temperature changed to {e.NewTemperature}°C");
    }
}

class Program
{
    static void Main()
    {
        // Create a weather sensor
        var sensor = new WeatherSensor();

        // Create subscribers (displays)
        var consoleDisplay = new ConsoleDisplay();
        var guiDisplay = new GuiDisplay();
        var webDashboard = new WebDashboard();

        // Subscribe the displays to the sensor's temperature updates
        consoleDisplay.Subscribe(sensor);
        guiDisplay.Subscribe(sensor);
        webDashboard.Subscribe(sensor);

        // Simulate temperature changes
        sensor.Temperature = 25.5;
        sensor.Temperature = 26.0;
        sensor.Temperature = 24.8;
    }
}
```

In this example, we have a `WeatherSensor` acting as the publisher, and three different displays (`ConsoleDisplay`, `GuiDisplay`, and `WebDashboard`) acting as subscribers. The subscribers subscribe to the sensor's `TemperatureChanged` event and react to temperature updates as they occur.

When you run this program, you'll see that each display receives updates when the temperature changes, demonstrating the Publish-Subscribe Pattern in action.   
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
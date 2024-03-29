using InformationSystemHZS.Classes;

namespace InformationSystemHZS.Models;

public class Vehicle(
    string name,
    VehicleCharacteristics characteristics,
    double fuelConsumption,
    int speed,
    int capacity
)
{
    public string Name { get; set; } = name;
    public VehicleCharacteristics Characteristics { get; set; } = characteristics;
    public double FuelConsumption { get; set; } = fuelConsumption;
    public int Speed { get; set; } = speed;
    public int Capacity { get; set; } = capacity;
}
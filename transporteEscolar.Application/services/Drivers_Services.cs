using transporteEscolar.Domain;

public class Drivers_Services : DataInterface<DriverDto>
{
    public Result Add(DriverDto element)
    {
        if (string.IsNullOrWhiteSpace(element.name) || 
            element.salary <= 0 ||
            string.IsNullOrWhiteSpace(element.license) ||
            string.IsNullOrWhiteSpace(element.vehicle))
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else if(element.vehicle != "carro" || element.vehicle != "autobus" || element.vehicle != "minivan"){
            return new Result(false, "el transporte debe ser carro, autobus o minivan");
        }
        else{
            Driver driver = new Driver();
            driver.name = element.name;
            driver.salary = element.salary;
            driver.license = element.license;
            driver.vehicle = element.vehicle;
            
            DriverData driversData = new DriverData();
            driversData.Add(driver);
            return new Result(true, "agregado correctamente");
        }
    }

    public IEnumerable<DriverDto> All()
    {
        List<DriverDto> drivers = new List<DriverDto>();
        DriverData DriverData = new DriverData();
        foreach(Driver driver in DriverData.All()){
            DriverDto driverDto = new DriverDto();
            driverDto.name = driver.name;
            driverDto.salary = driver.salary;
            driverDto.license = driver.license;
            driverDto.vehicle = driver.vehicle;
            drivers.Add(driverDto);
        }

        return drivers;
    }

    public Result Delete(int id)
    {
        DriverData driverData = new DriverData();
        if(driverData.All().Any(s => s.id != id)){
            return new Result(false, "ese id no existe");
        }
        else{
            driverData.Delete(id);
            return new Result(true, "eliminado correctamente");
        }
    }

    public DriverDto Get(int id)
    {
        DriverDto driverDto = new DriverDto();
        DriverData DriverData = new DriverData();
        if(DriverData.All().Any(s => s.id != id)){
            driverDto.name = "no existe";
            driverDto.salary = 0;
            driverDto.license = "no existe";
            driverDto.vehicle = "no existe";
        }
        else{
            var driver = DriverData.Get(id);
            driverDto.name = driver.name;
            driverDto.salary = driver.salary;
            driverDto.license = driver.license;
            driverDto.vehicle = driver.vehicle;           
        }

        return driverDto;
    }

    public Result Update(DriverDto element)
    {
        if (string.IsNullOrWhiteSpace(element.name) || 
            element.salary <= 0 ||
            string.IsNullOrWhiteSpace(element.license) ||
            string.IsNullOrWhiteSpace(element.vehicle))
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else if(element.vehicle != "carro" || element.vehicle != "autobus" || element.vehicle != "minivan"){
            return new Result(false, "el transporte debe ser carro, autobus o minivan");
        }
        else{
            Driver driver = new Driver();
            driver.name = element.name;
            driver.salary = element.salary;
            driver.license = element.license;
            driver.vehicle = element.vehicle;
            
            DriverData driversData = new DriverData();
            driversData.Add(driver);
            return new Result(true, "agregado correctamente");
        }
    }
}
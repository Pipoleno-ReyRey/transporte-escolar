using transporteEscolar.Domain;

public class Drivers_Services : ServicesInterface<DriverDto>
{
    public async Task<Result> Add(DriverDto element)
    {
        if (string.IsNullOrWhiteSpace(element.name) || 
            element.salary <= 0 ||
            string.IsNullOrWhiteSpace(element.license) ||
            string.IsNullOrWhiteSpace(element.vehicle))
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else if(element.vehicle == "carro" || element.vehicle == "bus" || element.vehicle == "camion"){
            Driver driver = new Driver();
            driver.name = element.name;
            driver.salary = element.salary;
            driver.license = element.license;
            driver.vehicle = element.vehicle;
            
            DriverData driversData = new DriverData();
            await driversData.Add(driver);
            return new Result(true, "agregado correctamente");
        }else{
            return new Result(false, "no fue agregado");
        }
    }

    public async Task<IEnumerable<DriverDto>> All()
    {
        List<DriverDto> drivers = new List<DriverDto>();
        DriverData DriverData = new DriverData();
        foreach(Driver driver in await DriverData.All()){
            DriverDto driverDto = new DriverDto();
            driverDto.id = driver.id;
            driverDto.name = driver.name;
            driverDto.salary = driver.salary;
            driverDto.license = driver.license;
            driverDto.vehicle = driver.vehicle;
            drivers.Add(driverDto);
        }

        return drivers;
    }

    public async Task<Result> Delete(int id)
    {
        DriverData driverData = new DriverData();
        var driverDataAll = await driverData.All();
        bool state = true;
        string message = "";
        foreach(Driver driver in driverDataAll){
            if(driver.id == id){
                await driverData.Delete(id);
                state = true;
                message = "conductor eliminado";
                break;
            }
            else{
                state = false;
                message = "el conductor no pudo ser eliminado";
            }
        }
        
        return new Result(state, message);
    }

    public async Task<DriverDto> Get(int id)
    {
        DriverDto driverDto = new DriverDto();
        DriverData DriverData = new DriverData();
        var driverDataAll = await DriverData.All();
        foreach(Driver driver in driverDataAll){
            if(driver.id == id){
                var driver1 = await DriverData.Get(id);
                driverDto.id = id;
                driverDto.name = driver1.name;
                driverDto.salary = driver1.salary;
                driverDto.license = driver1.license;
                driverDto.vehicle = driver1.vehicle;
                break;
            }
            else{
                driverDto.name = "no existe";
                driverDto.salary = 0;
                driverDto.license = "no existe";
                driverDto.vehicle = "no existe";        
            }
        }
        return driverDto;
    }

    public async Task<Result> Update(DriverDto element, int id)
    {
        if (string.IsNullOrWhiteSpace(element.name) || 
            element.salary <= 0 ||
            string.IsNullOrWhiteSpace(element.license) ||
            string.IsNullOrWhiteSpace(element.vehicle))
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else if(element.vehicle == "carro" || element.vehicle == "bus" || element.vehicle == "camion"){
            DriverData driverData = new DriverData();
            var driversAll = await driverData.All();
            foreach(Driver driver in driversAll){
            if(driver.id == id){
                Driver driver1 = new Driver();
                driver1.id = id;
                driver1.name = element.name;
                driver1.salary = element.salary;
                driver1.license = element.license;
                driver1.vehicle = element.vehicle;
                await driverData.Update(driver1);
                break;
            }
        }
            return new Result(true, "el conductor fua actualizado");
        } else{
            return new Result(false, "el conductor no pudo ser actualizado");
        }
    }
}
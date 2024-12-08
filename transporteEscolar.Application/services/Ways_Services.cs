using transporteEscolar.Domain;

public class Ways_Services : ServicesInterface<WayDto>
{
    public async Task<Result> Add(WayDto element)
    {
        if (string.IsNullOrEmpty(element.origin) || 
            string.IsNullOrEmpty(element.destiny) ||
            element.cost <= 0 ||
            element.time == null)
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else{
            Ways way = new Ways();
            way.origin = element.origin;
            way.destiny = element.destiny;
            way.time = element.time;
            way.cost = element.cost;
            
            WaysData waysData = new WaysData();
            await waysData.Add(way);
            return new Result(true, "agregado correctamente");
        }
    }

    public async Task<IEnumerable<WayDto>> All()
    {
        List<WayDto> ways = new List<WayDto>();
        WaysData waysData = new WaysData();
        var waysList = await waysData.All();
        foreach(Ways way in waysList){
            WayDto wayDto = new WayDto();
            wayDto.id = way.id;
            wayDto.origin = way.origin;
            wayDto.destiny = way.destiny;
            wayDto.time = way.time;
            wayDto.cost = way.cost;
            ways.Add(wayDto);
        }

        return ways;
    }

    public async Task<Result> Delete(int id)
    {
        WaysData waysData = new WaysData();
        var waysAll = await waysData.All();
        bool state = true;
        string x = "";
        foreach(Ways way in waysAll){
            if(way.id == id){
                await waysData.Delete(id);
                state = true;
                x = "ruta eliminada correctamente";
                break;
            } else{
                state = false;
                x = "el id no existe";
            }
        }

        return new Result(state, x);
    }

    public async Task<WayDto> Get(int id)
    {
        List<WayDto> ways = new List<WayDto>();
        WaysData waysData = new WaysData();
        var waysList = await waysData.All();
        WayDto wayDto = new WayDto();
        foreach(Ways way in waysList){
            if(way.id == id){
                wayDto.id = way.id;
                wayDto.origin = way.origin;
                wayDto.destiny = way.destiny;
                wayDto.time = way.time;
                wayDto.cost = way.cost;
                break;
            }
        }

        if(wayDto.origin == null){
            wayDto.origin = "no existe";
            wayDto.destiny = "no existe";
            wayDto.time = TimeOnly.MinValue;
            wayDto.cost = 0;
        }

        return wayDto;
    }

    public async Task<Result> Update(WayDto element, int id)
    {
        if (string.IsNullOrEmpty(element.origin) || 
            string.IsNullOrEmpty(element.destiny) ||
            element.cost <= 0 ||
            element.time == null)
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else{
            WaysData waysData = new WaysData();
            var waysList = await waysData.All();
            bool state = true;
            string message = "";
            foreach(Ways way in waysList){
                if(way.id == id){
                    Ways way1 = new Ways();
                    way1.id = id;
                    way1.origin = element.origin;
                    way1.destiny = element.destiny;
                    way1.time = element.time;
                    way1.cost = element.cost;
                    await waysData.Update(way1);
                    state = true;
                    message = $"ruta actualizada";
                    break;
                }
                else{
                        state = false;
                        message = $"el id no existe";
                    }
            }
            return new Result(state, message);              
        }
    }

    public async Task<IEnumerable<WayDto>> GetDriverWayService(int id)
    {
        List<WayDto> ways = new List<WayDto>();
        WaysData waysData = new WaysData();
        var waysList = await waysData.GetDriverWays(id);
        foreach(Ways way in waysList){
            WayDto wayDto = new WayDto();
            wayDto.id = way.id;
            wayDto.origin = way.origin;
            wayDto.destiny = way.destiny;
            wayDto.time = way.time;
            wayDto.cost = way.cost;
            ways.Add(wayDto);
        }

        return ways;
    }
}

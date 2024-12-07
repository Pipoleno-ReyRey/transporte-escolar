using Azure.Core;
using transporteEscolar.Domain;
public class Pays_Services : PaysInterface<PayDto>
{
    public async Task<Result> Add(PayDto element)
    {
        StudentsData studentsData = new StudentsData();
        var studentsDataAll = await studentsData.All();
        bool state = true;
        string message = "";
        foreach(Student student in studentsDataAll){
            if(element.count > 0 && element.studentId == student.id){

                Pays pay = new Pays();
                pay.count = element.count;
                pay.studentId = element.studentId;
                PayData payData = new PayData();
                await payData.Add(pay);

                Student student1 = new Student();
                student1.id = student.id;
                student1.name = student.name;
                student1.address = student.address;
                student1.telefono = student.telefono;
                student1.email = student.email;
                student1.WayId = student.WayId;
                student1.debt = student.debt - element.count;
                if(student1.debt < 0){student1.debt = 0;}

                await studentsData.Update(student1);
                BillsData billData = new BillsData();
                Bills bill = await billData.Get(student.id);
                if(bill.count <= element.count){
                    bill.count = 0;
                    bill.state = true;
                } else{
                    bill.count -= element.count;
                    bill.state = false;
                }
                await billData.Update(bill);

                state = true;
                message = "pago realizado";

                break;
            } else{
                state = false;
                message = "pago fallido";
            }
        }

        return new Result(state, message);
        
    }

    public async Task<IEnumerable<PayDto>> All(int studentId)
    {
        List<PayDto> pays = new List<PayDto>();
        PayData payData = new PayData();
        foreach(Pays pay in await payData.All()){
            if(pay.studentId == studentId){
                PayDto payDto = new PayDto(); 
                payDto.count = pay.count;
                payDto.datePay = pay.datePay;
                payDto.studentId = pay.studentId;
                pays.Add(payDto);
            }
        }
        
        return pays;
    }
}
import axios from 'axios'

// Author: Phạm Văn Đạt
// function: Service xử lý chi tiết khách hhangf
//  created time: 16:20 15/08/2022
export class CustomerDetailService{

    url= "https://localhost:44355/api/CustomerDetail/";


    async getByCustomerId(id){
       try{
            return axios.get(this.url+id).then((res) => {
                return res.data;
             })
             .catch((e) => {
               console.log(e);
             });         

       }catch(error){
        console.log(error);
       }
    }
}




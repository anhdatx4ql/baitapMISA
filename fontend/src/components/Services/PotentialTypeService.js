import axios from 'axios'

// Author: Phạm Văn Đạt
// function: Service xử lý thao tác loại tiềm năng
//  created time: 11:00 16/08/2022
export class PotentialTypeService {

    url= "https://localhost:44355/api/PotentialType/";


    // lấy danh sách loại tiềm năng
    async getAll(){
       try{
            return axios.get(this.url).then((res) => {
                return res.data.data;
             })
             .catch((e) => {
               console.log(e);
             });         

       }catch(error){
        console.log(error);
       }
    }

    // lấy danh sách loại tiềm năng theo tên
    async getByName(search){
        try{
            return axios.get(this.url+search).then((res) => {
                return res.data.data;
             })
             .catch((e) => {
               console.log(e);
             });         
        }catch(error){
            console.log(error);
        }
    }
}

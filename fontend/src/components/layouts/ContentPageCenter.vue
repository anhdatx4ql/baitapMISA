<template>
  <div class="content-child content-center">
    <div class="table-container" ref="TableData">
      <!-- loading -->
      <div class="content-background-icon-loading">
        <div class="background-icon-loading"></div>
      </div>

      <div class="table-header">
        <div class="thead">
          <div class="tr">
            <div class="th tr-child-center">
              <span class="th-icon-sum background-icon-header-sum"></span>
            </div>
            <div class="th tr-child-center">
              <input
                type="checkbox"
                id="listCustomerId"
                ref="listCustomerId"
                :checked="false"
                :value="listCustomerId"
              />
              <label
                for="listCustomerId"
                :class="(listCustomerId.length == 0)
                    ? 'background-icon-checked-table'
                    : 'background-icon-ckecked-header'
                "
                @click="ClickCheckboxHeader"
                ref="checkedHeader"
              ></label>
            </div>
            <div class="th">Thẻ</div>
            <div class="th">Xưng hô</div>
            <div class="th">Họ và tên</div>
            <div class="th">Chức danh</div>
            <div class="th">ĐT di động</div>
            <div class="th">ĐT cơ quan</div>
            <div class="th">Email cơ quan</div>
            <div class="th">Email cá nhân</div>
            <div class="th">Tổ chức</div>
            <div class="th">Mã số thuế</div>
            <div class="th">Doanh thu</div>
            <div class="th">Địa chỉ</div>
          </div>
        </div>
      </div>
      <div class="table-content">
        <div class="tbody" ref="tbody">
          <div
            class="tr"
            v-for="c in customer"
            :key="c.customerId"
          >
            <div
              class="td tr-child-center"
              :value="c.customerId"
              @click="ClickUpdate(c)"
            >
              <span class="background-icon-update"></span>
            </div>
            <div class="td tr-child-center">
              <input
                type="checkbox"
                class="trCheckbox"
                :value="c.customerId"
                :id="c.customerId"
              />
              <label
                :for="c.customerId"
                class="background-icon-checked-table"
                @click="ClickHandlerChecked"
              ></label>
            </div>
            <div class="td"></div>
            <!-- thẻ -->
            <div class="td">{{ c.vocativeName ? TitleCase(c.vocativeName) : "-" }}</div>
            <div class="td fullNameTable" @click="ClickDetail(c.customerId)" style="pointer:cursor;">{{ c.fullName ? TitleCase(c.fullName) : "-" }}</div>
            <div class="td">{{ c.positionName ? TitleCase(c.positionName) : "-" }}</div>
            <div class="td container-phone-number">
              <span class="background-icon-phone-center icon-font-16"></span>
              {{ c.customerPhoneNumber ? c.customerPhoneNumber : "-" }}
            </div>
            <div class="td container-phone-number">
              <span class="background-icon-phone-center icon-font-16"></span>
              {{ c.companyPhoneNumber ? c.companyPhoneNumber : "-" }}
            </div>
            <div class="td">{{ c.companyEmail ? c.companyEmail : "-" }}</div>
            <div class="td">{{ c.customerEmail ? c.customerEmail : "-" }}</div>
            <div class="td">{{ c.organization ? TitleCase(c.organization) : "-" }}</div>
            <div class="td">{{ c.taxCode ? c.taxCode : "-" }}</div>
            <div class="td">{{ c.turnoverName ? TitleCase(c.turnoverName) : "-" }}</div>
            <div class="td">{{ c.address ? TitleCase(c.address) : "-" }}</div>
          </div>
        </div>
      </div>
    </div>

    <div class="table-button">
      <div class="table-button-left">
        Tổng số: <span class="table-button-left-text">{{totalCount}}</span>
      </div>

      <div class="table-button-right">
        <div class="table-button-right-chlid">
          <div class="form-container-content-child-item-input">
                <div class="combobox" id="paging">
                  <div
                    class="combobox-child combobox-content"
                    @click="HandlerClickCombobox"
                    check="false"
                  >
                    <div
                      class="combobox-content-select combobox-child"
                      ref="pagingId"
                    >
                      {{pageSize}} bản ghi trên Trang
                    </div>
                    <div
                      class="
                        combobox-content-icon
                        button-icon-up-black
                        icon-font-16
                        combobox-child
                      "
                    ></div>
                  </div>
                  <div class="combobox-child combobox-data" ref="pageSize">
                    <div class="combobox-data-child">
                      <div
                        class="combobox-data-child-content"
                        v-for="v in ListPaging"
                        :key="v[0]"
                        :class="{
                          selected:
                            v[0] == pageSize? true : false,
                        }"
                        @click="HandlerClickComboboxData"
                        :value="v[0]"
                      >
                        <div class="combobox-data-child-content-text">
                           {{v[1]}} bản ghi trên Trang
                        </div>
                        <div
                          class="background-icon-checked icon-font-16"
                          :style="
                            v.Code == pageSize
                              ? 'display:inline-block'
                              : 'display:none'
                          "
                        ></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
        </div>

        <div class="table-button-right-chlid">
          <button class="table-button-right-chlid-button button button-background-white" 
          :class="{'background-disabled':(checkPreData==true)?false:true}"
          @click="ClickPreFirstData">
            <span class="background-icon-first icon-font-16"></span>
          </button>
          <button class="table-button-right-chlid-button button button-background-white" 
          :class="{'background-disabled':(checkPreData==true)?false:true}"
          @click="ClickPreData">
            <span  class="background-icon-pre icon-font-16"></span>
          </button>
          <div class="table-button-right-chlid-text">
            <span>{{startColumn}}</span> đến <span>{{endColumn}}</span>
          </div>
          <button 
          class="table-button-right-chlid-button button button-background-white"
          :class="{'background-disabled':(checkNextData==true)?false:true}"
          @click="ClickNextData">
          
            <span class="background-icon-next icon-font-16"></span>
          </button>
          <button class="table-button-right-chlid-button button button-background-white" 
          :class="{'background-disabled':(checkNextData==true)?false:true}"
          @click="ClickNextLastData()">
            <span class="background-icon-last icon-font-16"></span>
          </button>
        </div>
       
      </div>
    </div>

    <div
      class="buttonHideShow buttonHideShowLeft"
      @click="checkLeft = !checkLeft"
    >
      <span class="background-icon-arrow-left icon-font-16"></span>
    </div>
    <div
      class="buttonHideShow buttonHideShowRight"
      @click="checkRight = !checkRight"
    >
      <span class="background-icon-arrow-right icon-font-16"></span>
    </div>
  </div>
</template>

<script>
  //  xử lý hiển thị text
import { TitleCase } from "../../js/HandlerString";
// các hàm xử lý click combobox
// nhúng status code
import { StatusCode } from "../Models/StatusCode";

// các hàm xử lý click combobox
import {  
  ClickShowHideComboboxData,
  SelectValueComboboxData,
  HandlerScroll, 
  HandlerClickButtonArrow 
} from "../../js/HandlerCombobox";

// loading
import { UnLoading, Loading } from "../../js/Loading";

// nhúng service xử lý customer
import { CustomerService } from "../Services/CustomerService";

// nhúng service xử lý customer detail
import { CustomerDetailService } from "../Services/CustomerDetailService";

export default {
  name: "ContentPageCenter",
  data() {
    return {
      checkShow: false,
      checkLeft: false,
      checkRight: false,
      customer: "",
      listCustomerId: [],
      checkReloadData: false,
      currentPage:1,
      keyword:null,
      pageSize:10,
      checkPreData:false,
      checkNextData:false,
      totalCount:0,
      startColumn:0,
      endColumn:0,
      totalPages:0,
      ListPaging: new Map(),
      checkLoadData: false,
      customerDetail:{}
    };
  },  
  props: {
    searchCustomerCurrent: {},
    checkShowFormData: Boolean,
    checkLoadCustomerData: Boolean,
    ArrayObjectData:{},
    removeListCustomerId:Boolean
  },
  created() {

    this.ListPaging.set(10,10);
    this.ListPaging.set(20,20);
    this.ListPaging.set(50,50);
    this.ListPaging.set(100,100);
    // function: lấy thông tin c;
    this.checkLoadData= true;
  },
  methods: {
    // xử lý hiển thị text
    TitleCase,

     /**
     * Author:Phạm Văn Đạt
     * function:Hiển thị dữ liệu trang đầu tiên
     */
    ClickPreFirstData(){
      if(this.checkPreData == true){
        this.currentPage = 1;
        this.checkLoadData = true;
      }
    },

   /**
     * Author:Phạm Văn Đạt
     * function:  Hiển thị dữ liệu trang trước đó
     */
    ClickPreData(){
      if(this.checkPreData == true){
        this.currentPage =  this.currentPage-1;
        this.checkLoadData = true;

      }
    },

  /**
     * Author:Phạm Văn Đạt
     * function: Hiển thị dữ liệu trang tiếp theo
     */
    ClickNextData(){
      if(this.checkNextData == true){
        this.currentPage =  this.currentPage+1;
        this.checkLoadData = true;
      }
    },

  /**
   * Hiển thị dữ liệu cuối cùng
   */
    ClickNextLastData(){
      if(this.checkNextData == true){
        this.currentPage =  this.totalPages;
        this.checkLoadData = true;
      }
    },
    /**
     * Author: Phạm Văn Đạt
     * function:  xử lý đóng form data
     * created time: 11:28 17/08/2022
     */
    HandlerClickCombobox: function (event) {
      try {

        // hiển thị combobox data
        ClickShowHideComboboxData(event);
      } catch (error) {
        console.log(error);
      }
    },

     /**
     * Author: Phạm Văn Đạt
     * function:  xử lý click combobox data
     */
     HandlerClickComboboxData: function (event) {
      try {

        // chuyển curent và page về default 
        this.currentPage =  1;

        // lấy giá trị El
        let El = event.target;
        while(!El.getAttribute("value")){
            El = El.parentNode;
        }

        this.pageSize = El.getAttribute("value");
        SelectValueComboboxData(event);
      } catch (error) {
        console.log(error);
      }
    },

    //Author: Phạm Văn Đạt
    // function: kích nút để hiển thị thông tin chi tiết
    // created time: 11:59 15/08/2022
    ClickDetail: function (customerId) {
      let _CustomerDetailService = new CustomerDetailService();
      _CustomerDetailService.getByCustomerId(customerId).then((res) => {
        console.log(res)
        if (res.statusCode == StatusCode.GetSuccess ) this.customerDetail = res.data;
      });
    },

    //Author: Phạm Văn Đạt
    // function: kích nút để hiển thị form chỉnh sửa thông tin
    // created time: 18:55 16/08/2022
    ClickUpdate: function (CustomerInfo) {
      this.checkShow = !this.checkShow;
      this.$emit("ShowFormData", this.checkShow);
      this.$emit("CustomerInfo", CustomerInfo);
    },

     /**
     * Author: Phạm Văn Đạt
     * function:  xử lý click checked input
     */
    ClickHandlerChecked: function (event) {
      // xử lý thêm, xóa id trong mảng xử lý xóa, sửa
      let El = event.target.parentNode.getElementsByTagName("input")[0];
      if (El) {
        let check = El.checked;
        let ElValue = El.getAttribute("value");
        if (ElValue) {
          if (check == false) {
            this.listCustomerId.push(ElValue);
          } else {
            console.log(El.getAttribute("value"));
            // lấy vị trí phần tử đó trong mảng
            const valueRemove = this.listCustomerId.indexOf(ElValue);
            // xóa phân tử đó đi
            this.listCustomerId.splice(valueRemove, 1);
          }

        }
      }

    },

     /**
     * Author: Phạm Văn Đạt
     * function: xử lý checkbox header
     */
    ClickCheckboxHeader: function (event) {
      let ElChecked = event.target;
      let El = event.target.parentNode.getElementsByTagName("input")[0];
      if (El) {
        let ElsCheckbox = this.$refs.tbody.getElementsByClassName("trCheckbox");
        // nếu tồn tại các bản đã checked thì xóa hết
        if (this.listCustomerId.length > 0) {
          // lấy ra hết các ô kiểm tra xem cái nào checked thì bỏ

          for (let item of ElsCheckbox) {
            if (item.checked == true) {
              item.checked = false;
            }
          }
          // xóa gết phần tử trong mảng
          if (ElChecked.classList.contains("background-icon-ckecked-header")) {
             El.checked = true;
            this.listCustomerId.splice(0, this.listCustomerId.length);
          }
        } else {

          // thay đổi icon 

          for (let item of ElsCheckbox) {
            if (item.checked == false) {
              const value =  item.getAttribute("value");
              if(value)
                this.listCustomerId.push(value)
              item.checked = true;
            }
          }
        }
      }

    },

     /**
     * Author: Phạm Văn Đạt
     * function: Load dữ liệu khi lọc
     */
    async LoadDataFilter(Data,keyword,currentPage,pageSize){
      
      // loading
      if(this.$refs.TableData != undefined)
        Loading(this.$refs.TableData);
      let _CustomerService = new CustomerService();
       await _CustomerService.PagingFilterCustomer(Data,keyword,currentPage,pageSize).then((res) => {
        if(res.statusCode == StatusCode.GetSuccess){
          this.customer = res.data.data;
          this.totalCount = res.data.totalCount
          this.startColumn = (res.data.currentPageNumber-1)*res.data.pageSize +1;
          this.endColumn =  (res.data.currentPageNumber*res.data.pageSize < this.totalCount)?res.data.currentPageNumber*res.data.pageSize:this.totalCount;
          this.checkPreData = res.data.hasPrePage;
          this.totalPages = res.data.totalPages;
          this.checkNextData = res.data.hasNextpage;

          // nếu số thứ tự bản ghi đầu tiên < số thứ tự bản ghi cuối cùng-> load về trang đầu tiên
          if(this.startColumn > this.endColumn){
            this.currentPage = 1;
            this.checkLoadData  = true;
          }

          // load xong bỏ hết các dòng đã chọn
          this.listCustomerId.splice(0, this.listCustomerId.length);

          // bỏ checked
          let checkboxs = document.getElementsByClassName("trCheckbox");
          
          // chuyển background checked
          if (checkboxs) {
          
              for (let item of checkboxs) {
                if (item.checked == true) {
                  item.checked = false;
                }
              }

            } 
          }
          
        });
        UnLoading(this.$refs.TableData);
    },

    /**
     * Author: Phạm Văn Đạt
     * function: Load dữ liệu hiển thì và tìm kiếm
     */
    async LoadData(keyword,currentPage,pageSize){
      if(this.$refs.TableData != undefined)
        Loading(this.$refs.TableData);
      let _CustomerService = new CustomerService();
       await _CustomerService.PagingCustomer(keyword,currentPage,pageSize).then((res) => {
        console.log(res)
        if(res.statusCode == StatusCode.GetSuccess){
          this.customer = res.data.data;
          this.totalCount = res.data.totalCount
          this.startColumn = (res.data.currentPageNumber-1)*res.data.pageSize +1;
          this.endColumn =  (res.data.currentPageNumber*res.data.pageSize < this.totalCount)?res.data.currentPageNumber*res.data.pageSize:this.totalCount;
          this.checkPreData = res.data.hasPrePage;
          this.totalPages = res.data.totalPages;
          this.checkNextData = res.data.hasNextpage;
          // load xong bỏ hết các dòng đã chọn
          this.listCustomerId.splice(0, this.listCustomerId.length);

          this.CloseCheckedCheckBox();

          }
          
        });
        UnLoading(this.$refs.TableData);
    },

    /**
     * Author: Phạm Văn Đạt
     * Function: Hủy bỏ checked
     */
    CloseCheckedCheckBox(){
      // bỏ checked
      let checkboxs = document.getElementsByClassName("trCheckbox");
          
          // chuyển background checked
          if (checkboxs) {
          
              for (let item of checkboxs) {
                if (item.checked == true) {
                  item.checked = false;
                }
              }

              let itemCheckbox = this.$refs.checkedHeader;
              let itemInputCheckbox = this.$refs.listCustomerId;
              console.log(itemCheckbox)
              console.log(itemInputCheckbox)

              if(itemCheckbox != undefined && itemInputCheckbox != undefined){
                  console.log("vao fay")
                if(itemInputCheckbox.checked == true){
                  itemInputCheckbox.checked = false;
                }
              }
    }
  }
     
  },
  // theo dõi các biến thay đổi và thực hiện hàm nếu có
  watch: {
    /**
     * Author: Phạm Văn Đạt
     * function: bỏ hết checked
     */
     removeListCustomerId(){
      console.log(this.checkedHeader)

      if(this.$refs.checkedHeader){
        console.log("bo chon")

        this.CloseCheckedCheckBox();

        if(this.$refs.listCustomerId){
          if(this.$refs.listCustomerId.checked == true){
            this.$refs.listCustomerId.checked = false;
          }
        }

        this.listCustomerId = [];

      }


    },

    /**
     * Author: Phạm Văn Đạt
     * function: theo dõi chi tiết khách hàng
     */
    customerDetail(value){
      this.$emit("customerDetails",value);
    },

    /**
     * Author: Phạm Văn Đạt
     * function: theo dõi mảng giá trị lặp
     */
    ArrayObjectData(){
      // PagingFilterCustomer
      this.LoadDataFilter(this.ArrayObjectData,this.keyword, this.currentPage,this.pageSize);
      console.log(this.ArrayObjectData.length)
      // gọi api
    },

    /**
     * Author: Phạm Văn Đạt
     * function: kiểm tra check load dữ liệu
     */
    async checkLoadData(){
      if(this.checkLoadData == true){
        if(this.ArrayObjectData.length == 0){
          await this.LoadData(this.keyword, this.currentPage,this.pageSize);
        }
        else{
          await this.LoadDataFilter(this.ArrayObjectData,this.keyword, this.currentPage,this.pageSize);
        }
        this.checkLoadData=false;

      }
  
    },

     /**
     * Author: Phạm Văn Đạt
     * function: Load lại dữ liệu mỗi khi chọn trang mới
     */
    pageSize(){
      this.checkLoadData = true;
    },

     /**
     * Author: Phạm Văn Đạt
     * function: theo dõi danh sách khách hàng
     */
    Customer() {
      UnLoading(this.$refs.TableData);
    },

    /**
     * Author: Phạm Văn Đạt
     * function: hiển thị, ẩn side bar bên trái
     */
    checkLeft() {
      HandlerClickButtonArrow(this.checkLeft, this.checkRight, event);
    },

     /**
     * Author: Phạm Văn Đạt
     * function: hiển thị, ẩn side bar bên phải
     */
    checkRight() {
      HandlerClickButtonArrow(this.checkLeft, this.checkRight, event);
    },

    /**
     * Author: Phạm Văn Đạt
     * function: lấy thông tin khách hàng
     */
    searchCustomerCurrent() {
      this.keyword = this.searchCustomerCurrent;
      this.checkLoadData = true;
    },

    /**
     * Author: Phạm Văn Đạt
     * function: theo  dõi mảng để xóa danh sác, update
     */
    listCustomerId: {
      handler: function (val) {
        this.$emit("listCustomerId", val);
      },
      deep: true
    
    },

    /**
     * Author: Phạm Văn Đạt
     * function: check load form data
     */
    checkLoadCustomerData(){
       Loading(this.$refs.TableData)
        // load lại dữ liệu
        this.checkLoadData = true;
        UnLoading(this.$refs.TableData)
        this.$emit("checkLoadCustomerData",false);
    }
   
  },
  mounted() {
    /**
     * Author: Phạm Văn Đạt
     * function: Xử lý scroll
     */
    HandlerScroll();
  },
};
</script>

<style>

</style>
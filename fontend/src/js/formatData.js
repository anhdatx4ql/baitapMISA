import moment from 'moment'


export function formatDate(value){
    return moment(String(value)).format('DD/MM/YYYY')
  }
import { EmailValidator } from "@angular/forms";

export class Register {
    isSelected:boolean;
    firstname:string;
    lastname:string;
    email:EmailValidator;
    born:Date;
    admission:Date;
    practicingArea:string;
    practicingLocation:string;
    position:string;
    state:string;
    pan:string;
    address:string;
    contactnumber:number;
    isEdit:boolean;

}
export const UserColumns =[
  
  {
    key:'syncId',
    type:'text',
    label:'SyncID',
    disabled:true,
  },
  {
    key:'firstname',
    type:'text',
    label:"FirstName",
    disabled:true,
  },
  {
    key:'lastname',
    type:'text',
    label:"LastName",
    disabled:true,
  },
  {
    key:'email',
    type:'email',
    label:"Email",
    disabled:false,
    required:true
  },{
    key:'admission',
    type:'date',
    label:"Admission",
    disabled:true,
  },{
    key:'born',
    type:'date',
    label:"Born",
    disabled:true,
  },{
    key:'practicingArea',
    type:'text',
    label:"Area",
    disabled:true,
  },{
    key:'practicingLocation',
    type:'text',
    label:"Location",
    disabled:true,
  },{
    key:'position',
    type:'text',
    label:"Position",
    disabled: true,
  },{
    key:'pan',
    type:'text',
    label:"PAN",
    disabled:false,
  },
  {
    key:'address',
    type:'text',
    label:"Address",
    disabled:false
  },
  {
    key:'state',
    type:'text',
    label:"State",
    disabled:false
  },
  {
    key:'contactNumber',
    type:'number',
    label:"Contact",
    disabled:false
  },
  {
    key:'isEdit',
    type:'isEdit',
    label:'Action',
  }

]

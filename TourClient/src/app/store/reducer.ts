import {PAYMENT_DETAIL_REQUEST} from '../payment-details/shared/payment-detail.actions';
import { IAppState } from './IAppState';

const paymentDetails = [];

const initialState: IAppState = {
    paymentDetails: paymentDetails,
}

function getPayDetails(state, action):IAppState{
    return Object.assign({}, state,{
        paymentDetails: action.paymentDetails
    });
}
export function reducer(state=initialState, action){
    switch(action.type){
        case(PAYMENT_DETAIL_REQUEST):  
            console.log('i am in the reducer function');
            console.log(JSON.stringify(action.paymentDetails));
            return getPayDetails(state, action);
        default:
            return state;

    }
    
}
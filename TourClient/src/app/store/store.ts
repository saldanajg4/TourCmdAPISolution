import { createStore, applyMiddleware, compose } from 'redux';
// import { createStore, applyMiddleware, compose } from 'redux';
import { IAppState } from './IAppState';
import { reducer } from './reducer';

declare var window:any;

// const devToolsExtension: GenericStoreEnhancer = (window.devToolsExtension)
//     ? window.devToolsExtension() : (f) => f;

// export const store = createStore<IAppState>(reducer,
//     compose(devToolsExtension) as GenericStoreEnhancer);


export const store = createStore(reducer);
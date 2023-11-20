"use strict";(self.webpackChunktest=self.webpackChunktest||[]).push([[709],{3905:(e,t,n)=>{n.d(t,{Zo:()=>l,kt:()=>m});var r=n(7294);function i(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function a(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?a(Object(n),!0).forEach((function(t){i(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):a(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function p(e,t){if(null==e)return{};var n,r,i=function(e,t){if(null==e)return{};var n,r,i={},a=Object.keys(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||(i[n]=e[n]);return i}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(i[n]=e[n])}return i}var c=r.createContext({}),s=function(e){var t=r.useContext(c),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},l=function(e){var t=s(e.components);return r.createElement(c.Provider,{value:t},e.children)},g="mdxType",u={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},d=r.forwardRef((function(e,t){var n=e.components,i=e.mdxType,a=e.originalType,c=e.parentName,l=p(e,["components","mdxType","originalType","parentName"]),g=s(n),d=i,m=g["".concat(c,".").concat(d)]||g[d]||u[d]||a;return n?r.createElement(m,o(o({ref:t},l),{},{components:n})):r.createElement(m,o({ref:t},l))}));function m(e,t){var n=arguments,i=t&&t.mdxType;if("string"==typeof e||i){var a=n.length,o=new Array(a);o[0]=d;var p={};for(var c in t)hasOwnProperty.call(t,c)&&(p[c]=t[c]);p.originalType=e,p[g]="string"==typeof e?e:i,o[1]=p;for(var s=2;s<a;s++)o[s]=n[s];return r.createElement.apply(null,o)}return r.createElement.apply(null,n)}d.displayName="MDXCreateElement"},4914:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>c,contentTitle:()=>o,default:()=>u,frontMatter:()=>a,metadata:()=>p,toc:()=>s});var r=n(7462),i=(n(7294),n(3905));const a={sidebar_position:3},o="Read String",p={unversionedId:"mapping-string-in-object",id:"mapping-string-in-object",title:"Read String",description:"Mapping object in string",source:"@site/docs/mapping-string-in-object.md",sourceDirName:".",slug:"/mapping-string-in-object",permalink:"/Spinner/docs/mapping-string-in-object",draft:!1,editUrl:"https://github.com/Daniel-iel/Spinner/docs/mapping-string-in-object.md",tags:[],version:"current",sidebarPosition:3,frontMatter:{sidebar_position:3},sidebar:"tutorialSidebar",previous:{title:"Write Object",permalink:"/Spinner/docs/mapping-object-in-string"},next:{title:"Type Parser",permalink:"/Spinner/docs/mapping-string-into-type"}},c={},s=[{value:"Mapping object in string",id:"mapping-object-in-string",level:2},{value:"Instantiate",id:"instantiate",level:2},{value:"Read from String",id:"read-from-string",level:2}],l={toc:s},g="wrapper";function u(e){let{components:t,...n}=e;return(0,i.kt)(g,(0,r.Z)({},l,n,{components:t,mdxType:"MDXLayout"}),(0,i.kt)("h1",{id:"read-string"},"Read String"),(0,i.kt)("h2",{id:"mapping-object-in-string"},"Mapping object in string"),(0,i.kt)("p",null,"To configure an object, utilize the ",(0,i.kt)("inlineCode",{parentName:"p"},"ObjectMapper")," and ",(0,i.kt)("inlineCode",{parentName:"p"},"ReadProperty")," properties for mapping the string layout."),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-csharp"},"  [ObjectMapper(length: 50)]\n  public class NothingReader\n  {\n    [ReadProperty(start:1, length: 19 )]        \n    public string Name { get; private set; }\n\n    [ReadProperty(start: 20, length: 30)]        \n    public string WebSite { get; private set; }\n  }\n")),(0,i.kt)("h2",{id:"instantiate"},"Instantiate"),(0,i.kt)("p",null,"To map your object as a string, instantiate the ",(0,i.kt)("inlineCode",{parentName:"p"},"Spinner")," class, specifying the object type with T"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-csharp"},"  Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();\n")),(0,i.kt)("h2",{id:"read-from-string"},"Read from String"),(0,i.kt)("p",null,"After configuring the object, you need to call the ",(0,i.kt)("inlineCode",{parentName:"p"},"ReadFromString")," method to read a string and convert it to an object."),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-csharp"},'  var obj = spinnerReader.ReadFromString("             spinner            www.spinner.com.br");\n')))}u.isMDXComponent=!0}}]);
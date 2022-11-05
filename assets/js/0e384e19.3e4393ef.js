"use strict";(self.webpackChunktest=self.webpackChunktest||[]).push([[671],{9881:(e,n,t)=>{t.r(n),t.d(n,{contentTitle:()=>a,default:()=>c,frontMatter:()=>s,metadata:()=>o,toc:()=>p});var r=t(7462),i=(t(7294),t(3905));const s={sidebar_position:1},a="Get started",o={unversionedId:"intro",id:"intro",isDocsHomePage:!1,title:"Get started",description:"Prerequisites",source:"@site/docs/intro.md",sourceDirName:".",slug:"/intro",permalink:"/docs/intro",editUrl:"https://github.com/Daniel-iel/Spinner/docs/intro.md",tags:[],version:"current",sidebarPosition:1,frontMatter:{sidebar_position:1},sidebar:"tutorialSidebar",next:{title:"Write Object",permalink:"/docs/mapping-object-in-string"}},p=[{value:"Prerequisites",id:"prerequisites",children:[],level:2},{value:"Installation",id:"installation",children:[],level:2},{value:"Import Spinner",id:"import-spinner",children:[],level:2},{value:"Configuring an Object",id:"configuring-an-object",children:[],level:2},{value:"Using Spinner",id:"using-spinner",children:[],level:2}],l={toc:p};function c(e){let{components:n,...t}=e;return(0,i.kt)("wrapper",(0,r.Z)({},l,t,{components:n,mdxType:"MDXLayout"}),(0,i.kt)("h1",{id:"get-started"},"Get started"),(0,i.kt)("h2",{id:"prerequisites"},"Prerequisites"),(0,i.kt)("p",null,"Spinner is only compatible with ",(0,i.kt)("strong",{parentName:"p"},"dotnet core 3.0, 3.1")," and  ",(0,i.kt)("strong",{parentName:"p"},"dotnet 5.0, 6.0.")),(0,i.kt)("h2",{id:"installation"},"Installation"),(0,i.kt)("p",null,"Spinner is distributed as a NuGet package. See the ",(0,i.kt)("strong",{parentName:"p"},(0,i.kt)("a",{parentName:"strong",href:"https://www.nuget.org/packages/Spinner/"},"spinner"))," for more information."),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-shell"},"dotnet add package Spinner\n")),(0,i.kt)("h2",{id:"import-spinner"},"Import Spinner"),(0,i.kt)("p",null,"After install the package, you need import Spinner class into your class."),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-csharp"},"using Spinner;\n")),(0,i.kt)("h2",{id:"configuring-an-object"},"Configuring an Object"),(0,i.kt)("p",null,"To have a good knowledge to how configure your object, you can see the ",(0,i.kt)("strong",{parentName:"p"},(0,i.kt)("a",{parentName:"strong",href:"/docs/mapping-object-in-string"},"example"))),(0,i.kt)("h2",{id:"using-spinner"},"Using Spinner"),(0,i.kt)("p",null,"Run ",(0,i.kt)("strong",{parentName:"p"},"WriteAsString")," to get mapped object as string or call ",(0,i.kt)("strong",{parentName:"p"},"WriteAsSpan")," to get the result as span:"),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-csharp"},' var nothing = new Nothing("spinner", "www.spinner.com.br");\n var spinner = new Spinner<Nothing>(nothing);\n var stringResponse = spinner.WriteAsString();   \n //stringresponse = "              spinner            www.spinner.com.br"\n')),(0,i.kt)("pre",null,(0,i.kt)("code",{parentName:"pre",className:"language-csharp"},' var nothing = new Nothing("spinner", "www.spinner.com.br");\n var spinner = new Spinner<Nothing>(nothing);\n var spanResponse = spinner.WriteAsSpan();   \n //spanResponse = "              spinner            www.spinner.com.br"\n')))}c.isMDXComponent=!0}}]);
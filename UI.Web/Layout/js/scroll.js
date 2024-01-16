
dir=0 // 0 = left 1 = right
speed=3
imageSize=100  // % set to zero to use fixedWidth and fixedHeight values
fixedWidth=100 // set a fixed width
fixedHeight=60 // set a fixed height
spacerWidth=20 // space between images

popupLeft= 100 // pixels
popupTop= 100 // pixels

biggest=0
ieBorder=0
totalWidth=0
hs3Timer=null

preload=new Array()
for(var i=0;i<myImages.length;i++){
preload[i]=new Image()
preload[i].src=myImages[i][0]
}

function initHS3(){
//alert("INIT");
scroll1=document.getElementById("scroller1")

for(var j=0;j<myImages.length;j++){

scroll1.innerHTML+='<img id="pic'+j+'" src="'+preload[j].src+'" alt="'+myImages[j][2]+'" title="'+myImages[j][2]+'" onclick="showBigPic('+j+')">'

if(imageSize!=0){ // use percentage size
newWidth=preload[j].width/100*imageSize
newHeight=preload[j].height/100*imageSize
}
else{ // use fixed size
newWidth=fixedWidth
newHeight=fixedHeight
}

document.getElementById("pic"+j).style.width=newWidth+"px"
document.getElementById("pic"+j).style.height=newHeight+"px"

if(document.getElementById("pic"+j).offsetHeight>biggest){
biggest=document.getElementById("pic"+j).offsetHeight
}

document.getElementById("pic"+j).style.marginLeft=spacerWidth+"px"

totalWidth+=document.getElementById("pic"+j).offsetWidth+spacerWidth

}

totalWidth+=1

for(var k=0;k<myImages.length;k++){ // vertically center images
document.getElementById("pic"+k).style.marginBottom = (biggest-document.getElementById("pic"+k).offsetHeight)/2+"px"
}

if(document.getElementById&&document.all){
ieBorder=parseInt(document.getElementById("scrollbox").style.borderTopWidth)*2
}

document.getElementById("scrollbox").style.height=biggest+ieBorder+"px"

scroll1.style.width=totalWidth+"px"

scroll2=document.getElementById("scroller2")
scroll2.innerHTML=scroll1.innerHTML
scroll2.style.left= (-scroll1.offsetWidth)+"px"
scroll2.style.top= -scroll1.offsetHeight+"px"
scroll2.style.width=totalWidth+"px"

if(dir==1){
speed= -speed
}

scrollHS3()
}


function scrollHS3(){
if(paused==1){return}
clearTimeout(hs3Timer)

scroll1Pos=parseInt(scroll1.style.left)
scroll2Pos=parseInt(scroll2.style.left)

scroll1Pos-=speed
scroll2Pos-=speed

scroll1.style.left=scroll1Pos+"px"
scroll2.style.left=scroll2Pos+"px"

hs3Timer=setTimeout("scrollHS3()",50)

if(dir==0){
if(scroll1Pos< -scroll1.offsetWidth){
scroll1.style.left=scroll1.offsetWidth+"px"
}

if(scroll2Pos< -scroll1.offsetWidth){
scroll2.style.left=scroll1.offsetWidth+"px"
}
}

if(dir==1){
if(scroll1Pos>parseInt(document.getElementById("scrollbox").style.width)){
scroll1.style.left=scroll2Pos+ (-scroll1.offsetWidth)+"px"
}

if(scroll2Pos>parseInt(document.getElementById("scrollbox").style.width)){
scroll2.style.left=scroll1Pos+ (-scroll2.offsetWidth)+"px"
}
}

}

st=null
function pause(){
clearTimeout(hs3Timer)
clearTimeout(st)
}

function reStartHS3(){
clearTimeout(st)
st=setTimeout("scrollHS3()",100)
}

paused=0
picWin=null

function showBigPic(p){

if(myImages[p][1]!=""){
paused=1

if(picWin&&picWin.open&&!picWin.closed){picWin.close()} // if picWin exists close it

if(myImages[p][1].indexOf("jpg")!=-1){
bigImg=new Image()
bigImg.src=myImages[p][1]

data="\n<center>\n<img src='"+bigImg.src+"'>\n</center>\n"

var winProps = "left= "+popupLeft+", top = "+popupTop+", width="+(bigImg.width+20)+", height="+(bigImg.height+20)+", scrollbars=no, toolbar=no, directories=no, menu bar=no, resizable=yes, status=no"

picWin=window.open("","win1",winProps)
picWin.document.write("<HTML>\n<HEAD>\n<TITLE></TITLE>\n")
picWin.document.write("</HEAD>\n")
picWin.document.write("<BODY bgcolor='black' topmargin=10px leftmargin=10>\n")
picWin.document.write("<div id=\"display\">"+data+"</div>")
picWin.document.write("\n</BODY>\n</HTML>")
}
else{
picWin=window.open(myImages[p][1])
}

}

}

window.onfocus=function(){
paused=0
scrollHS3()
}

onunload=function(){ // close the popup when leaving page
if(picWin&&picWin.open&&!picWin.closed){
picWin.close()
}
}
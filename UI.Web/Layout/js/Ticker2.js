/*Example message arrays for the two demo scrollers*/

//var tickercontent=new Array()
//tickercontent[0]='<a href="http://www.javascriptkit.com">JavaScript Kit</a><br />Comprehensive JavaScript tutorials and over 400+ free scripts!'
//tickercontent[1]='<a href="http://www.codingforums.com">Coding Forums</a><br />Web coding and development forums.'
//tickercontent[2]='<a href="http://www.cssdrive.com" target="_new">CSS Drive</a><br />Categorized CSS gallery and examples.'

//var tickercontent2=new Array()
//tickercontent2[0]='<a href="http://www.news.com">News.com: Technology and business reports</a>'
//tickercontent2[1]='<a href="http://www.cnn.com">CNN: Headline and breaking news 24/7</a>'
//tickercontent2[2]='<a href="http://news.bbc.co.uk">BBC News: UK and international news</a>'

/***********************************************
* DHTML Ticker script- © Dynamic Drive (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit http://www.dynamicdrive.com/ for this script and 100s more.
***********************************************/

function domticker(content, divId, divClass, delay, fadeornot)
{
    this.content=content
    this.tickerid=divId //ID of master ticker div. Message is contained inside first child of ticker div
    this.delay=delay //Delay between msg change, in miliseconds.
    this.mouseoverBol=0 //Boolean to indicate whether mouse is currently over ticker (and pause it if it is)
    this.pause=0 // IF PAUSE IS ON OR NOT
    this.dir=1 // direction 1 : Next, 2: Previous, 0: Not Set
    this.pointer=1
    this.opacitystring=(typeof fadeornot!="undefined")? "width: 100%; filter:progid:DXImageTransform.Microsoft.alpha(opacity=100); -moz-opacity: 1" : ""
    if (this.opacitystring!="") 
        this.delay+=800 //add 1/2 sec to account for fade effect, if enabled
    
    this.opacitysetting=0.2 //Opacity value when reset. Internal use.
    document.write('<div id="'+divId+'" class="'+divClass+'"> <div style="'+this.opacitystring+'">'+content[0]+'</div></div>')
    var instanceOfTicker=this
    setTimeout(function(){instanceOfTicker.initialize()}, delay)
}

domticker.prototype.initialize=function()
{
    var instanceOfTicker=this
    this.contentdiv=document.getElementById(this.tickerid).firstChild //div of inner content that holds the messages
document.getElementById(this.tickerid).onmouseover=function(){instanceOfTicker.mouseoverBol=1}
document.getElementById(this.tickerid).onmouseout=function(){instanceOfTicker.mouseoverBol=0}
//alert("LENGHT: "+this.content.length);
if(this.content.length>1)
{
    this.rotatemsg();
}
}

domticker.prototype.rotatemsg=function(){
var instanceOfTicker=this
if (this.mouseoverBol==1 || this.pause==1) //if mouse is currently over ticker, do nothing (pause it)
setTimeout(function(){instanceOfTicker.rotatemsg()}, 100)
else{
this.fadetransition("reset") //FADE EFFECT- RESET OPACITY
this.contentdiv.innerHTML=this.content[this.pointer]
this.fadetimer1=setInterval(function(){instanceOfTicker.fadetransition('up', 'fadetimer1')}, 100) //FADE EFFECT- PLAY IT
this.pointer=(this.pointer<this.content.length-1)? this.pointer+1 : 0
setTimeout(function(){instanceOfTicker.rotatemsg()}, this.delay) //update container
}
}

// -------------------------------------------------------------------
// fadetransition()- cross browser fade method for IE5.5+ and Mozilla/Firefox
// -------------------------------------------------------------------

domticker.prototype.fadetransition=function(fadetype, timerid)
{
//alert("THIS: "+this.contentdiv);

try
{
    var contentdiv=this.contentdiv
    if (fadetype=="reset")
        this.opacitysetting=0.2
    if (contentdiv.filters && contentdiv.filters[0])
    {
        if (typeof contentdiv.filters[0].opacity=="number") //IE6+
            contentdiv.filters[0].opacity=this.opacitysetting*100
        else //IE 5.5
            contentdiv.style.filter="alpha(opacity="+this.opacitysetting*100+")"
    }
    else if (typeof contentdiv.style.MozOpacity!="undefined" && this.opacitystring!="")
    {
    contentdiv.style.MozOpacity=this.opacitysetting
    }
    else
        this.opacitysetting=1
        
    if (fadetype=="up")
        this.opacitysetting+=0.2
    if (fadetype=="up" && this.opacitysetting>=1)
        clearInterval(this[timerid])
}
catch(ex)
{
}
}

/*************************************************************/
///////////////// MAHMOUD TAHOON ADDED METHODS ///////////////
/*************************************************************/
domticker.prototype.pauseTicker=function(){
var instanceOfTicker=this;
instanceOfTicker.pause=1;
//alert("TICKER PAUSE");

}
domticker.prototype.playTicker=function(){
var instanceOfTicker=this;
instanceOfTicker.pause=0;
//alert("TICKER PLAY");
}

domticker.prototype.nextTicker=function()
{
    var instanceOfTicker=this
    this.fadetransition("reset") //FADE EFFECT- RESET OPACITY
    this.contentdiv.innerHTML=this.content[this.pointer]
    this.fadetimer1=setInterval(function(){instanceOfTicker.fadetransition('up', 'fadetimer1')}, 100) //FADE EFFECT- PLAY IT
    this.pointer=(this.pointer<this.content.length-1)? this.pointer+1 : 0
    if(instanceOfTicker.dir !=1)
    {
        this.pointer=(this.pointer<this.content.length-1)? this.pointer+1 : 0
        instanceOfTicker.dir=1;
    }
    instanceOfTicker.pauseTicker();
}
domticker.prototype.prevTicker=function()
{
    var instanceOfTicker=this
    this.pointer=(this.pointer>0)? this.pointer-1 : this.content.length-1
    if(instanceOfTicker.dir !=2)
    {
        this.pointer=(this.pointer>0)? this.pointer-1 : this.content.length-1
        instanceOfTicker.dir=2;
    }
    
    this.fadetransition("reset") //FADE EFFECT- RESET OPACITY
    this.contentdiv.innerHTML=this.content[this.pointer]
    this.fadetimer1=setInterval(function(){instanceOfTicker.fadetransition('up', 'fadetimer1')}, 100) //FADE EFFECT- PLAY IT
    
    instanceOfTicker.pauseTicker();
}

////new domticker(name_of_message_array, CSS_ID, CSS_classname, pause_in_miliseconds, optionalfadeswitch)

//new domticker(tickercontent, "domticker", "Ticker", 5000, "fadeit")
//document.write("<br />")
//new domticker(tickercontent2, "domticker2", "someclass", 3000)

#pragma strict

var target : GameObject;

 target = gameObject;

var startDelay : float = 1; 

var fadeDuration : float = 10; 

var fadeDirection = -1; //direction should be 1 or -1

 

function Start(){

    if (fadeDirection == 1) target.renderer.material.color.a = 0.0;

    FadeAlpha();

}

 

function FadeAlpha(){ 

   yield WaitForSeconds(startDelay);

   while (true){

        target.renderer.material.color.a += Time.deltaTime/fadeDuration * fadeDirection;

        if (target.renderer.material.color.a < 0 || target.renderer.material.color.a > 1) return;

        yield;

    }

}
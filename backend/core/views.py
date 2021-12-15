from PIL.Image import NONE
from django.db import reset_queries
from django.shortcuts import redirect, render
from .models import *
from .FaceAuthentication import FaceRegognation, base64ToImg
from .PhoneAuthentication import phone_authentication as phoneAuthentication,confirm_otp
from django.http import JsonResponse
import json
from django.views.decorators.csrf import csrf_exempt
import base64
from atm_system.settings import BASE_DIR

@csrf_exempt
def Login(request):
    if request.method == 'POST':
        account = request.POST.get('account',None)
        try:
            check = UserDetails.objects.get(account_number=account)
        except:
            return JsonResponse({
                "status":"error","message":"Invalid account number"
            })
        return JsonResponse({
                "status":"ok","message":"successfull","data":check.serializer()
            }) 
    return JsonResponse({
                "status":"error","message":"POST request required"
            })

@csrf_exempt
def FaceAuthentication(request):
    if request.method == 'POST':

        account_id = request.POST.get('account_id',None)
        canvasData = request.POST.get('canvasData', None)

        if account_id is None:
            return JsonResponse({
                "status":"error","message":"account_id requried"
            })
        
        if canvasData is None:
            return JsonResponse({
                "status":"error","message":"canvasData requried"
            })
        try:
            user = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({
                "status":"error","message":"Invalid account id"
            })

        try:
            photo = user.profile_photo.url
            if len(photo) != 0:
                pass
        except:
            return JsonResponse({
                    "status":"error","message":"You Have not setup you Face Authentication,Try a diffeirent option"
                })

        

        canvasData = canvasData.replace(' ','+')
        try:
            print(photo)
            result = FaceRegognation(photo,canvasData)
            print(result)
        except:
            return JsonResponse({
                    "status":"error","message":"Unable to verify your face"
                })

        if result == True:
            user.last_authentication = '1'
            user.save()
            print('authorized')
            return JsonResponse({
                    "status":"ok","message":"authorized"
                })
        else:
            print('un authorized')
            return JsonResponse({
                    "status":"error","message":"unauthorized"
                })
    else:
        return JsonResponse({
                    "status":"error","message":"POST request required"
                })

    

@csrf_exempt
def submitFace(request):
    if request.method == 'POST':
        account_id = request.POST.get('account_id',None)
        amount = request.POST.get('amount',None)
        canvasData = request.POST.get('canvasData', None)
        if account_id is None:
            return JsonResponse({"status":"error","message":"account_id is required"})
        
        try:
            user = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({
                "status":"error",
                "message":"invalid account id"
            })
        

        if amount is None:
            return JsonResponse({
                "status":"error",
                "message":"amount not found"
            })

        if canvasData is None:
            return JsonResponse({
                "status":"error","message":"canvasData requried"
            })


        photo = user.profile_photo.url

        canvasData = canvasData.replace(' ','+')

        try:
            result = FaceRegognation(photo,canvasData)
        except:
            f = open('a.txt','a')
            f.write(canvasData)
            f.close()
            return JsonResponse({
                    "status":"error","message":"Unable to verify your face"
                })

        if result == True:
            if float(amount) >= float(user.balance):
                return JsonResponse({"status":"error","message":"Insufficiant amount"})
            user.balance = float(user.balance)-float(amount)
            user.save()
            return JsonResponse({
            "status":"ok",
            "message":"Payment Successfull!!",
            "data":user.serializer()
        })
        else:
            return JsonResponse({
            "status":"error",
            "message":"Unable to verify your face"
        })

    return JsonResponse({"status":"error","message":"POST request required"})


@csrf_exempt
def api_send_otp(request):
    if request.method == 'POST':
        account_id = request.POST.get('account_id',None)
        
        if account_id is None:
            return JsonResponse({"status":"error","message":"account_id is required"})
        
        try:
            user = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({"status":"error","message":"Invalid account id"})

        try:
            phone_number = user.phone_number
        except:
            return JsonResponse({"status":"error","message":"Phone is not setup"})

        result = phoneAuthentication(account_id)

        if result == True:
            return JsonResponse({"status":"ok","message":"Successfull","data":{"account_id":user.account_number,"phone_number":user.phone_number}})
        return JsonResponse({"status":"error","message":"something went wrong"})
    return JsonResponse({"status":"error","message":"POST request required"})


@csrf_exempt
def submit_otp(request):
    if request.method == 'POST':
        account_id = request.POST.get('account_id',None)
        otp = request.POST.get("otp",None)
        amount = request.POST.get('amount',None)
        
        
        if account_id is None:
            return JsonResponse({
                "status":"error",
                "message":"account_id not found"
            })
        
        if otp is None:
            return JsonResponse({
                "status":"error",
                "message":"otp not found"
            })


        
        result = confirm_otp(account_id,otp)

        if result == True:
            user = UserDetails.objects.get(account_number=account_id)
            if amount is not None:
                if float(amount) >= float(user.balance):
                    return JsonResponse({"status":"error","message":"Insufficiant amount"})
                user.balance = float(user.balance)-float(amount)
                user.save()
                return JsonResponse({
                "status":"ok",
                "message":"Payment Successfull!!",
                "data":user.serializer()
            })
            else:
                return JsonResponse({
                "status":"ok",
                "message":"OTP verified!!",
                "data":user.serializer()
            })
        else:
            return JsonResponse({
            "status":"error",
            "message":"Invalid OTP"
        })
    return JsonResponse({"status":"error","message":"POST request required"})


@csrf_exempt
def api_check_balance(request):
    if request.method == 'POST':

        account_id = request.POST.get('account_id',None)
        
        if account_id is None:
            return JsonResponse({"status":"error","message":"account_id  is required!!"})
        try:
            result = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({"status":"error","message":"Invalid account id"})

        return JsonResponse({"status":"ok","message":"ok","balance":result.balance})
    return JsonResponse({"status":"error","message":"POST request required"})


@csrf_exempt
def getFingerprint(request):
    if request.method == 'POST':

        account_id = request.POST.get('account_id',None)
        
        if account_id is None:
            return JsonResponse({"status":"error","message":"account_id  is required!!"})
        try:
            result = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({"status":"error","message":"Invalid account id"})
        try:
            image_src = result.finger_photo.url
        except:
            return JsonResponse({"status":"error","message":"Finger Print Not setuped"})
        image_src = BASE_DIR+image_src
        result = convertImgToBase64(image_src)
        return JsonResponse({"status":"ok","message":"successfull!!","base64String":str(result)})
    return JsonResponse({"status":"error","message":"POST request required"})


@csrf_exempt
def FingerPrintAuth(request):
    if request.method == 'POST':

        account_id = request.POST.get('account_id',None)
        
        if account_id is None:
            return JsonResponse({"status":"error","message":"account_id  is required!!"})
        try:
            result = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({"status":"error","message":"Invalid account id"})
        if result.auth_access == 'authorized':
            return JsonResponse({"status":"ok","message":"authorized"})    
        return JsonResponse({"status":"ok","message":"unauthorized"})    

    return JsonResponse({"status":"error","message":"POST request required"})





@csrf_exempt
def deductBalance(request):
    if request.method == 'POST':
        account_id = request.POST.get('account_id',None)
        amount = request.POST.get('amount',None)

        if account_id is None or amount is None:
            return JsonResponse({"status":"error","message":"account_id and amount is required!!"})
        try:
            result = UserDetails.objects.get(account_number=account_id)
        except:
            return JsonResponse({"status":"error","message":"Invalid account id"})
        
        if float(amount) >= float(result.balance):
            return JsonResponse({"status":"error","message":"Insufficiant amount"})
        result.balance = float(result.balance)-float(amount)
        result.save()
        return JsonResponse({
        "status":"ok",
        "message":"Payment Successfull!!",
        "data":result.serializer()
        })
    return JsonResponse({"status":"error","message":"POST request required"})




def convertImgToBase64(src):
    with open(src, "rb") as img_file:
        b64_string = base64.b64encode(img_file.read())

    b64_string = b64_string.decode('utf-8')
    return b64_string





﻿// System.Web.Sevices.Description.OperationCollection
// System.Web.Sevices.Description.OperationCollection.Add
// System.Web.Sevices.Description.OperationCollection.Contains
// System.Web.Sevices.Description.OperationCollection.IndexOf
// System.Web.Sevices.Description.OperationCollection.Remove
// System.Web.Sevices.Description.OperationCollection.Insert
// System.Web.Sevices.Description.OperationCollection.Item
// System.Web.Sevices.Description.OperationCollection.CopyTo

/*
   The following example demonstrates the usage of the
   'OperationCollection' class , its property 'Item' and its methods
   'Add', 'Contains', 'CopyTo', 'IndexOf', 'Insert' and 'Remove'.
   The input to the program is a WSDL file 'MathService_input_cs.wsdl'with
   information related to the 'Add' operation for the SOAP protocol,
   removed from it. It creates a new file 'MathService_new_cs.wsdl' with
   the added information about the 'Add' method.
*/

// <Snippet1>
using System;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Description;

class MyOperationCollectionSample
{
   public static void Main()
   {
      try
      {
         // Read the 'MathService_Input_cs.wsdl' file.
         ServiceDescription myDescription =
                     ServiceDescription.Read("MathService_Input_cs.wsdl");
         PortTypeCollection myPortTypeCollection =myDescription.PortTypes;
         // Get the 'OperationCollection' for 'SOAP' protocol.
// <Snippet2>
         OperationCollection myOperationCollection =
                                       myPortTypeCollection[0].Operations;
         Operation myOperation = new Operation();
         myOperation.Name = "Add";
         OperationMessage myOperationMessageInput =
                                  (OperationMessage) new OperationInput();
         myOperationMessageInput.Message = new XmlQualifiedName
                              ("AddSoapIn",myDescription.TargetNamespace);
         OperationMessage myOperationMessageOutput =
                                 (OperationMessage) new OperationOutput();
         myOperationMessageOutput.Message = new XmlQualifiedName(
                              "AddSoapOut",myDescription.TargetNamespace);
         myOperation.Messages.Add(myOperationMessageInput);
         myOperation.Messages.Add(myOperationMessageOutput);
         myOperationCollection.Add(myOperation);
// </Snippet2>

// <Snippet3>
// <Snippet4>
         if (myOperationCollection.Contains(myOperation))
         {
            Console.WriteLine("The index of the added 'myOperation' " +
                              "operation is : " +
                              myOperationCollection.IndexOf(myOperation));
         }
// </Snippet4>
// </Snippet3>

// <Snippet5>
// <Snippet6>
// <Snippet7>
         myOperationCollection.Remove(myOperation);
         // Insert the 'myOpearation' operation at the index '0'.
         myOperationCollection.Insert(0, myOperation);
         Console.WriteLine("The operation at index '0' is : " +
                           myOperationCollection[0].Name);
// </Snippet7>
// </Snippet6>
// </Snippet5>

// <Snippet8>
         Operation[] myOperationArray = new Operation[
                                             myOperationCollection.Count];
         myOperationCollection.CopyTo(myOperationArray, 0);
         Console.WriteLine("The operation(s) in the collection are :");
         for(int i = 0; i < myOperationCollection.Count; i++)
         {
            Console.WriteLine(" " + myOperationArray[i].Name);
         }
// </Snippet8>

         myDescription.Write("MathService_New_cs.wsdl");
      }
      catch(Exception e)
      {
         Console.WriteLine("Exception caught!!!");
         Console.WriteLine("Source : " + e.Source);
         Console.WriteLine("Message : " + e.Message);
      }
   }
}
// </Snippet1>